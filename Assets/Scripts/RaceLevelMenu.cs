using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class RaceLevelMenu : MonoBehaviour
{
    public TextMeshProUGUI HighscoreText; // Reference to the HighscoreText UI element
    public TextMeshProUGUI LevelNameText; // Reference to the LevelNameText UI element
    private static int selectedLevelIndex = 0; // Variable to hold the index of the selected level
    private List<string> levels = new List<string> { "Race1", "Race2" }; // List of level names



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
        LevelNameText.text = levels[levelIndex]; // Set the text of the LevelNameText UI element to the name of the selected level
    }

    public void StartRaceLevel()
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
}