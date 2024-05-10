using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour
{
    public Light directionalLight; // Reference to the directional light
    private bool hasInitialIntensityBeenSet = false;

    void Start()
    {
        StartCoroutine(IncreaseLightIntensityOverTime(0.01f, 1.0f, 1.5f)); // Changed duration to 1 second
    }

    void Update()
    {
        if (hasInitialIntensityBeenSet)
        {
            float currentTime = TimerAndMovement.currentTime;

            // Update the light intensity based on the current time
            if (currentTime <= 0)
            {
                directionalLight.intensity = 0.20f;
            }
            else if (currentTime < TimerAndMovement.TimerDuration)
            {
                // Scale the intensity to the new range (0.15 to 1.00)
                directionalLight.intensity = 0.15f + ((currentTime / TimerAndMovement.TimerDuration) * 0.85f);
            }
            else
            {
                directionalLight.intensity = 1;
            }
        }
    }

    IEnumerator IncreaseLightIntensityOverTime(float startIntensity, float endIntensity, float duration)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            directionalLight.intensity = Mathf.Lerp(startIntensity, endIntensity, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        directionalLight.intensity = endIntensity;
        hasInitialIntensityBeenSet = true;
    }
}