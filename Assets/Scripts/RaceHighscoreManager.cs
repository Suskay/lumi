using System.Collections.Generic;
using UnityEngine;

public class RaceHighscoreManager : MonoBehaviour
{
    private const int MaxHighscores = 10;

    public static RaceHighscoreManager Instance { get; private set; }

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

    public void SaveHighscore(string levelName, float score)
    {
        // Get the highscores for the level
        List<float> highscores = LoadHighscores(levelName);

        // Add the new score
        highscores.Add(score);

        // Sort the list in ascending order
        highscores.Sort();

        // If the list is too long, remove the worst score
        if (highscores.Count > MaxHighscores)
        {
            highscores.RemoveAt(MaxHighscores);
        }

        // Save the highscores
        for (int i = 0; i < highscores.Count; i++)
        {
            PlayerPrefs.SetFloat(levelName + "Highscore" + i, highscores[i]);
        }
        
        Debug.Log("Highscore saved: " + score);
    }

    public List<float> LoadHighscores(string levelName)
    {
        List<float> highscores = new List<float>();

        for (int i = 0; i < MaxHighscores; i++)
        {
            if (PlayerPrefs.HasKey(levelName + "Highscore" + i))
            {
                highscores.Add(PlayerPrefs.GetFloat(levelName + "Highscore" + i));
            }
            else
            {
                break;
            }
        }

        return highscores;
    }
}