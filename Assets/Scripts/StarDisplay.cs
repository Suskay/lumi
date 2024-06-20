using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    public Image[] stars; // Assign these in the inspector

    public Sprite activeStar; // Assign an active star sprite in the inspector
    public Sprite inactiveStar; // Assign an inactive star sprite in the inspector

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
}