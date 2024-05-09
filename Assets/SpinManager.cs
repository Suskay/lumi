using UnityEngine;

public class SpinManager : MonoBehaviour
{
    public float speed = 100f; // Rotation speed in degrees per second

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0); // Rotate around the z-axis
    }
}