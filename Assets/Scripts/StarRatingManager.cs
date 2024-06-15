using System.Collections.Generic;
using UnityEngine;

public class StarRatingManager : MonoBehaviour
{
    public static StarRatingManager Instance { get; set; }

    private Dictionary<string, (float twoStar, float threeStar)> starThresholds;

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

        starThresholds = new Dictionary<string, (float, float)>
        {
            { "Race1", (20f, 12f) }, // Two stars if < 60s, three stars if < 45s
            { "Race2", (90f, 75f) },
            { "Race3", (120f, 100f) },
            { "Race4", (150f, 130f) }
        };
    }

    public int GetStarRating(string levelName, float time)
    {
        if (starThresholds.ContainsKey(levelName))
        {
            var thresholds = starThresholds[levelName];
            if (time <= thresholds.threeStar)
            {
                Debug.Log("Three stars");
                return 3;
            }
                
            else if (time <= thresholds.twoStar)
            {
                Debug.Log("Two stars");
                return 2;
            }
        }

        Debug.Log("One star");
        return 1;
    }

    // Use this method to add new levels and their thresholds
    public void AddOrUpdateLevelThreshold(string levelName, float twoStarTime, float threeStarTime)
    {
        starThresholds[levelName] = (twoStarTime, threeStarTime);
    }
}