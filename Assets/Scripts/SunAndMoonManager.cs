using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SunAndMoonManager : MonoBehaviour
{
    public TimerAndMovement timerAndMovement;
    private RectTransform rectTransform;
    private int segments = 15; // Number of segments

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(RotateOverTime(-90, 90, 2));
    }

    private void Update()
    {
        float currentTime = TimerAndMovement.currentTime;
        float rotationZ;

        if (currentTime >= TimerAndMovement.TimerDuration)
        {
            rotationZ = 90;
        }
        else if (currentTime <= 0)
        {
            rotationZ = -90;
        }
        else
        {
            int currentSegment = (int)(currentTime / (TimerAndMovement.TimerDuration / segments));
            rotationZ = Mathf.Lerp(-90, 90, (float)currentSegment / segments);
        }

        rectTransform.localEulerAngles = new Vector3(0, 0, rotationZ);
    }

    IEnumerator RotateOverTime(float from, float to, float duration)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            float rotationZ = Mathf.Lerp(from, to, elapsed / duration);
            rectTransform.localEulerAngles = new Vector3(0, 0, rotationZ);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localEulerAngles = new Vector3(0, 0, to);
    }
}