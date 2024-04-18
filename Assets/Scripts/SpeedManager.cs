using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance { get; private set; }

    public float normalSpeed = 50f;
    public float maxSpeed = 75f;
    public float boostedSpeedMultiplier = 100f;
    public float maxBoostedSpeed = 100f; // Maximum speed when boosted


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

    public float GetRotationSpeed(float count, bool isBoosted)
    {
        float rotationSpeed = normalSpeed + Mathf.Pow(count, 2) + count * 2;

        if (isBoosted)
        {
            rotationSpeed *= boostedSpeedMultiplier;
            rotationSpeed = Mathf.Min(rotationSpeed, maxBoostedSpeed); // Limit the speed when boosted
            Debug.Log("Boosted speed: " + rotationSpeed);
        }
        else
        {
            rotationSpeed = Mathf.Min(rotationSpeed, maxSpeed);
        }

        return rotationSpeed;
    }
}
