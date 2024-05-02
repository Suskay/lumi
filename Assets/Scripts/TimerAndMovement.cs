using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerAndMovement : MonoBehaviour
{
    public static float timerDuration = 55f;
    public static float currentTime = 0f;
    private bool isTimerRunning = false;
    public Text timerText;
    private float totalTime = 0f;
    private FollowShadow followShadow;
    public Text gameOverText; // New UI Text element for game over message
    public Light directionalLight; // Reference to the directional light
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
                    GameManager.Instance.PlayGameOverSound();
                    StartCoroutine(StopAfterAnimation());

                    // Timer has reached zero, perform actions or stop the timer
                    Debug.Log("Timer has reached zero!");
                }
            }

            // Update the light intensity based on the current time
            if (currentTime <= 0)
            {
                directionalLight.intensity = 0.20f;
            }
            else if (currentTime < 55)
            {
                // Scale the intensity to the new range (0.15 to 1.00)
                directionalLight.intensity = 0.15f + ((currentTime / 55) * 0.85f);
            }
            else
            {
                directionalLight.intensity = 1;
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
        currentTime = timerDuration;
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
        gameOverText.text = "GAME OVER\nTotal score: " + ScoreManager.Instance.score +
                            "\nPress any key to restart or " +
                            "Escape to return to the Main Menu";

        // save the highscore
        SurvivalHighscoreManager.Instance.SaveHighscore(ScoreManager.Instance.score);
        GameManager.Instance.GameOver();
    }

    public static void IncreaseTimer(float timeToAdd)
    {
        // Increase the timer (add extra time, adjust as needed)
        currentTime += timeToAdd;
        Debug.Log("Timer increased! Current time: " + currentTime);
    }
}