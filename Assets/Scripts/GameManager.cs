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
        if (gameObject.tag == "Survival")
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

    private void Update()
    {
        if (IsGameOver)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !BlackoutManager.isAnimationPlaying)
            {
                RestartGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !BlackoutManager.isAnimationPlaying)
            {
                if (gameObject.tag == "Survival")
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else if (gameObject.tag == "Race")
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
        RaceStartCounter.isRaceStarted = false;
    }

    public Boolean BlockInput()
    {
        Debug.Log( IsGameOver == true || BlackoutManager.isAnimationPlaying);

        if (gameObject.CompareTag("Survival"))
        {
            Debug.Log(IsGameOver == true || BlackoutManager.isAnimationPlaying);
            return (IsGameOver == true || BlackoutManager.isAnimationPlaying == true);
        }
        else if (gameObject.CompareTag("Race"))
        {
           return IsGameOver || !RaceStartCounter.isRaceStarted;
        }

        return false;
    }
}