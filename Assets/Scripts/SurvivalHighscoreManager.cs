using System.Collections.Generic;
using UnityEngine;

public class SurvivalHighscoreManager : MonoBehaviour
{
    private const int MaxHighscores = 10;
    private const string HighscoreKey = "SurvivalHighscore";

    public static SurvivalHighscoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveHighscore(float score)
    {
        List<float> highscores = LoadHighscores();

        highscores.Add(score);
        highscores.Sort((a, b) => b.CompareTo(a)); // Sort in descending order

        if (highscores.Count > MaxHighscores)
        {
            highscores.RemoveAt(MaxHighscores);
        }

        for (int i = 0; i < highscores.Count; i++)
        {
            PlayerPrefs.SetFloat(HighscoreKey + i, highscores[i]);
        }

        Debug.Log("Highscore saved: " + score);
    }

    public List<float> LoadHighscores()
    {
        List<float> highscores = new List<float>();

        for (int i = 0; i < MaxHighscores; i++)
        {
            if (PlayerPrefs.HasKey(HighscoreKey + i))
            {
                highscores.Add(PlayerPrefs.GetFloat(HighscoreKey + i));
            }
            else
            {
                break;
            }
        }

        return highscores;
    }
}