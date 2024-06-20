using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class RaceLevelMenu : MonoBehaviour
{
    public TextMeshProUGUI HighscoreText; // Reference to the HighscoreText UI element
    public TextMeshProUGUI LevelNameText; // Reference to the LevelNameText UI element
    public RawImage LevelPreview; // Reference to the Level Preview UI element
    public Texture[] LevelThumbnails; // Array to hold the level thumbnails
    private static int selectedLevelIndex = 0; // Variable to hold the index of the selected level

    private List<string> levels = new List<string> // List of level names
    {
        "Race1",
        "Race2",
        "Race3",
        "Race4",
        "Race5",
        // "Race10"
    };

    public StarDisplay starDisplayComponent;

    public Button ConfirmLevelButton; // Reference to the ConfirmLevelButton UI element
    public TextMeshProUGUI ConfirmLevelButtonText; // Reference to the text component of the ConfirmLevelButton

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseSelectedLevel()
    {
        if (selectedLevelIndex < levels.Count - 1)
        {
            selectedLevelIndex++;
            Debug.Log("Increased Selected Level Index: " + selectedLevelIndex);
            SelectRaceLevel(selectedLevelIndex);
        }
    }

    public void DecreaseSelectedLevel()
    {
        if (selectedLevelIndex > 0)
        {
            selectedLevelIndex--;
            Debug.Log("Decreased Selected Level Index: " + selectedLevelIndex);
            SelectRaceLevel(selectedLevelIndex);
        }
    }

    private void SelectRaceLevel(int levelIndex)
    {
        DisplayHighscores(levels[levelIndex]);
        updateStarDisplay(levels[levelIndex]);
        LevelNameText.text =
            levels[levelIndex]; // Set the text of the LevelNameText UI element to the name of the selected level

        if (levelIndex >= 0 && levelIndex < LevelThumbnails.Length)
        {
            LevelPreview.texture = LevelThumbnails[levelIndex];
        }

        // Check if the level is unlocked
        bool isUnlocked = IsLevelUnlocked(levelIndex);
        if (isUnlocked)
        {
            // If the level is unlocked, display the thumbnail and button normally
            LevelPreview.color = Color.white;
            ConfirmLevelButton.interactable = true;
            ConfirmLevelButtonText.text = "START";
            // set to hexadecimal color code
            ConfirmLevelButtonText.color = Color.black; // Change text
        }
        else
        {
            // If the level is not unlocked, grey out the thumbnail and button
            LevelPreview.color = Color.gray;
            ConfirmLevelButton.interactable = false;
            ConfirmLevelButtonText.text = "LOCKED";
            ConfirmLevelButtonText.color = Color.gray;
        }
    }

    public void StartRaceLevel()
    {
        if (IsLevelUnlocked(selectedLevelIndex))
        {
            if (selectedLevelIndex >= 0 && selectedLevelIndex < levels.Count)
            {
                Debug.Log("Starting" + selectedLevelIndex);
                string levelToLoad = levels[selectedLevelIndex];
                Debug.Log("Selected Level Index: " + selectedLevelIndex);
                Debug.Log("Loading Level: " + levelToLoad);
                SceneManager.LoadScene(levelToLoad);
            }
            else
            {
                Debug.Log("No level selected");
            }
        }
        else
        {
            Debug.Log("Level is locked");
        }
    }

    private void Start()
    {
        SelectRaceLevel(selectedLevelIndex);
    }

    private void DisplayHighscores(string levelName)
    {
        List<float> highscores = RaceHighscoreManager.Instance.LoadHighscores(levelName);

        string highscoreString = "";
        for (int i = 0; i < highscores.Count && i < 5; i++)
        {
            int minutes = Mathf.FloorToInt(highscores[i] / 60F);
            int seconds = Mathf.FloorToInt(highscores[i] - minutes * 60);
            int hundredths = Mathf.FloorToInt((highscores[i] - minutes * 60 - seconds) * 100);
            highscoreString += string.Format("{0}. {1:00}:{2:00}:{3:00}\n", i + 1, minutes, seconds, hundredths);
        }

        HighscoreText.text = highscoreString;
    }

    public void updateStarDisplay(string level)
    {
        List<float> highscores = RaceHighscoreManager.Instance.LoadHighscores(level);
        int bestStars = 0;
        if (highscores.Count > 0)
            bestStars = StarRatingManager.Instance.GetStarRating(level, highscores[0]);

        Debug.Log(bestStars);

        Debug.Log("starDisplayComponent found!");
        starDisplayComponent.SetStars(bestStars);
    }

    private bool IsLevelUnlocked(int levelIndex)
    {
        // The first level is always unlocked
        if (levelIndex == 0)
        {
            return true;
        }

        // A level is unlocked if there is a highscore for the previous level
        string previousLevelName = levels[levelIndex - 1];
        List<float> highscores = RaceHighscoreManager.Instance.LoadHighscores(previousLevelName);
        return highscores.Count > 0;
    }
}