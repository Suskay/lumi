using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // This is a gamemanager
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }

    public static int CurrentLevelIndex { get; set; }
    
    public enum GameMode
    {
        Survival,
        Race,
        None
    }
    
    public String currentGameMode = "None"; // Current game mode

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            StartCoroutine(PlayThemeSongWhenReady());
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        if (gameObject.tag == "Survival")
        {
        }
        else if (gameObject.tag == "Race")
        {
        }
        // Display game over UI here if not done elsewhere
    }

    public void PlayGameOverSound()
    {
        SoundManager.Instance.PlayGameOverSound();
    }

    public void RestartGame()
    {
        // Reset the vignette
        if (currentGameMode == "Survival")
        {
            BlackoutManager blackoutManager = FindObjectOfType<BlackoutManager>();
            if (blackoutManager != null)
            {
                blackoutManager.ResetVignette();
            }
        }

        ResetManagers();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        IsGameOver = false;

        SoundManager.Instance.PlayThemeSong();
        // Reset any other necessary states or values here
    }

    public void LoadNextLevel()
    {
        if (CurrentLevelIndex < RaceLevelMenu.levels.Count - 1)
        {
            CurrentLevelIndex++;
            SceneManager.LoadScene(RaceLevelMenu.levels[CurrentLevelIndex]);
        }
        else
        {
            Debug.Log("No more levels to load");
        }
    }

    private void Update()
    {
        if (IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R) && !Input.GetKeyDown(KeyCode.Escape) && !BlackoutManager.isAnimationPlaying)
            {
                Debug.Log("Restarting");
                RestartGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !BlackoutManager.isAnimationPlaying)
            {
                Debug.Log("Pressing escape");
                if (currentGameMode == "Survival")
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else if (currentGameMode == "Race")
                {
                    SceneManager.LoadScene("RaceLevelMenu");
                }

                ResetManagers();
            }
        }
    }

    private IEnumerator PlayThemeSongWhenReady()
    {
        yield return new WaitUntil(() => SoundManager.Instance != null);
        SoundManager.Instance.PlayThemeSong();
    }

    public void ResetManagers()
    {
        ShadowManager.reset();
        SurvivalStatsManager.Reset();
        IsGameOver = false;
        RaceStartCounter.isRaceStarted = false;
    }

    public Boolean BlockInput()
    {
        Debug.Log("IsGameOver // BlockInput: " + IsGameOver);
        Debug.Log(gameObject.tag);
        if (currentGameMode == "Survival")
        {
            Debug.Log("IsGameOver: " + IsGameOver);

            return (IsGameOver == true || BlackoutManager.isAnimationPlaying == true);
        }
        else if (currentGameMode == "Race")
        {
            Debug.Log("IsGameOver: " + IsGameOver + "ISRaceStarted: " + RaceStartCounter.isRaceStarted);
            return IsGameOver || !RaceStartCounter.isRaceStarted;
        }

        return false;
    }
}