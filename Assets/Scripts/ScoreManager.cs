using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score = 0;
    public int jumpPoints = 100; // Points for successful jump
    public int timePoints = 25; // Points for time the game is running
    public int boostMultiplier = 3; // Multiplier for boosted mode

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddJumpPoints(int boostLevel)
    {
        score += (1+boostLevel)*jumpPoints;
    }

    public void AddTimePoints(int boostLevel)
    {
        score += (1+boostLevel)*timePoints;
    }
}