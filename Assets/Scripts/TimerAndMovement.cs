using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerAndMovement : MonoBehaviour
{
    public const float TimerDuration = 55f;
    public static float currentTime = 0f;
    private bool isTimerRunning = false;
    public Text timerText;
    private float totalTime = 0f;
    private FollowShadow followShadow;
    public Text gameOverText; // New UI Text element for game over message
    private float timeSinceLastPoint = 0f; // Time since the last point was
    public BlackoutManager blackoutManager;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Text component not found. Make sure the names are correct.");
        }

        if (gameOverText == null)
        {
            Debug.LogError("GameOverText component not found. Make sure the names are correct.");
        }

        // Get the FollowShadow script
        followShadow = GameObject.FindObjectOfType<FollowShadow>();

        // Start the timer when the script is first executed
        StartTimer();
    }

    void Update()
    {
        // Update the timer if it is running
        if (!GameManager.Instance.IsGameOver)
        {
            if (isTimerRunning)
            {
                currentTime -= Time.deltaTime;
                totalTime += Time.deltaTime;
                SurvivalStatsManager.IncrementTimeSurvived(Time.deltaTime);
                int currentTimeInt = (int)currentTime;
                // Update the UI Text component with the current time
                timerText.text = "Time: " + currentTimeInt.ToString("F0");

                timeSinceLastPoint += Time.deltaTime;
                if (timeSinceLastPoint >= 1f)
                {
                    timeSinceLastPoint -= 1f;
                    ScoreManager.Instance.AddTimePoints(ShadowManager.boostLevel);
                }

                timerText.text = ScoreManager.Instance.score.ToString();

                // Check if the timer has reached zero
                if (currentTime <= 0f)
                {
                    StopTimer();
                    SurvivalStatsManager.UpdateTotalPoints(ScoreManager.Instance.score);
                    SurvivalHighscoreManager.Instance.SaveHighscore(ScoreManager.Instance.score);
                    GameManager.Instance.GameOver();
                    GameManager.Instance.PlayGameOverSound();
                    StartCoroutine(StopAfterAnimation());

                    // Timer has reached zero, perform actions or stop the timer
                    Debug.Log("Timer has reached zero!");
                }
            }
        }
    }
    
    IEnumerator StopAfterAnimation()
    {
        yield return StartCoroutine(blackoutManager.AnimateVignette());
        DisplayGameOverMessage();
    }

    void StartTimer()
    {
        // Set the initial time on start
        currentTime = TimerDuration;
        isTimerRunning = true;
    }

    void StopTimer()
    {
        // Stop the timer
        isTimerRunning = false;
    }

    void DisplayGameOverMessage()
    {
        timerText.text = ""; // Format the time as needed;
        gameOverText.text = SurvivalStatsManager.GenerateGameOverText();
    }

    public static void IncreaseTimer(float timeToAdd)
    {
        // Increase the timer (add extra time, adjust as needed)
        // only increase time until a certain threshhold
        currentTime = currentTime < 65 ? currentTime + timeToAdd : 65;
        Debug.Log("Timer increased! Current time: " + currentTime);
    }
}