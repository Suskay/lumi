using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    public Image[] stars; // Assign these in the inspector

    public Sprite activeStar; // Assign an active star sprite in the inspector
    public Sprite inactiveStar; // Assign an inactive star sprite in the inspector

    public TextMeshProUGUI twoStarTime;
    public TextMeshProUGUI threeStarTime;

    private void Awake()
    {
        SetStars(0); // Default to no stars
    }

    public void SetStars(int count)
    {
        Debug.Log("setstars" + count);
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < count)
                stars[i].sprite = activeStar;
            else
                stars[i].sprite = inactiveStar;
        }
    }
    
    public void SetTimes(float time1, float time2)
    {
        this.twoStarTime.SetText(String.Format("00:{0:D2}:00", (int)time1));
        this.threeStarTime.SetText(String.Format("00:{0:D2}:00", (int)time2));
    }

}