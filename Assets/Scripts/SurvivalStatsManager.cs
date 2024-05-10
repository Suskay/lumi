using UnityEngine;

public class SurvivalStatsManager : MonoBehaviour
{
    public static SurvivalStatsManager Instance { get; private set; }
    public static float TimeSurvived { get; private set; }
    public static int TotalJumps { get; private set; }
    public static float TotalBoostTime { get; private set; }
    public static int CheckpointsReached { get; private set; }
    
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

    public static void IncrementTimeSurvived(float time)
    {
        TimeSurvived += time;
    }

    public static void IncrementTotalJumps()
    {
        TotalJumps++;
    }

    public static void IncrementTotalBoostTime(float time)
    {
        TotalBoostTime += time;
    }

    public static void IncrementCheckpointsReached()
    {
        CheckpointsReached++;
    }

    public static string GenerateGameOverText()
    {
        return $"GAME OVER\nTime Survived: {TimeSurvived:F2} seconds\nTotal Jumps: {TotalJumps}\nTotal Boost Time: {TotalBoostTime:F2} seconds\nCheckpoints Reached: {CheckpointsReached}\nPress any key to restart or Escape to return to the Main Menu";
    }
    
    public static void Reset()
    {
        TimeSurvived = 0;
        TotalJumps = 0;
        TotalBoostTime = 0;
        CheckpointsReached = 0;
    }
}