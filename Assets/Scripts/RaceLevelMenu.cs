using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class RaceLevelMenu : MonoBehaviour
{
    public TextMeshProUGUI HighscoreText; // Reference to the HighscoreText UI element

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartRaceLevel()
    {
        SceneManager.LoadScene("Race1");
    }

    private void Start()
    {
        DisplayHighscores("Race1"); // Replace "Race1" with the name of your level
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

        Debug.Log(HighscoreText.text);
        HighscoreText.text = highscoreString;
    }
}