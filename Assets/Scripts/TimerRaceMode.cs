using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerRaceMode : MonoBehaviour
{
    public Text timerText; // UI Text element to display the timer
    private float elapsedTime = 0f; // Time elapsed since the start of the game
    private bool isTimerRunning = false; // Whether the timer is currently running
    public Text gameOverText; // New UI Text element for game over message
    public GameObject textBackground; // Reference to the TextBackground plane
    public StarDisplay starDisplayComponent;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Text component not found. Make sure the names are correct.");
        }

        starDisplayComponent.gameObject.SetActive(false);
        StartCoroutine(StartTimer());
    }

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
            int hundredths = Mathf.FloorToInt((elapsedTime - minutes * 60 - seconds) * 100);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
        }
    }

    public IEnumerator StartTimer()
    {
        // Wait until the race has started
        yield return new WaitUntil(() => RaceStartCounter.isRaceStarted);

        elapsedTime = 0f;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        float elapsedTime = this.elapsedTime;
        var levelName = SceneManager.GetActiveScene().name;
        RaceHighscoreManager.Instance.SaveHighscore(levelName, elapsedTime);
        int starsEarned = StarRatingManager.Instance.GetStarRating(levelName, elapsedTime);

        //show the hidden star display component
        
        starDisplayComponent.gameObject.SetActive(true);
        starDisplayComponent.SetStars(starsEarned);

        var levelTimes = StarRatingManager.Instance.GetNeededTimesForLevel(levelName);
        starDisplayComponent.SetTimes(levelTimes.Item1, levelTimes.Item2);

        // Update game over text
        gameOverText.text = $"Time: {timerText.text}\nPress R to restart or Escape to return to the Main Menu";
        timerText.text = "";
        textBackground.SetActive(true);

        GameManager.Instance.GameOver();
    }
}