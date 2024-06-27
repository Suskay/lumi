using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class SurvivalMenu : MonoBehaviour
{
    public TextMeshProUGUI HighscoreText;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartSurvivalLevel()
    {
        GameManager.Instance.currentGameMode = "Survival";
        if (ChunksStorage.Instance != null)
        {
            SceneManager.LoadScene("Survival");
        }
        else
        {
            SceneManager.LoadScene("Chunks");
        }
    }

    private void Start()
    {
        DisplayHighscores();
    }

    private void DisplayHighscores()
    {
        List<float> highscores = SurvivalHighscoreManager.Instance.LoadHighscores();

        string highscoreString = "";
        for (int i = 0; i < highscores.Count && i < 5; i++)
        {
            highscoreString += string.Format("{0}. {1:N0}\n", i + 1, highscores[i]);
        }

        HighscoreText.text = highscoreString;
    }
}