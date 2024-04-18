using UnityEngine;

public class SunScript : MonoBehaviour
{
    public GameObject sun; // Assign your light source here
    public float radius = 5.0f;
    public float speed = 1.0f;

    private float angle = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change KeyCode.Space to your desired button
        {
            angle += speed * Time.deltaTime; // Increment angle
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            sun.transform.position = new Vector3(x, sun.transform.position.y, z) + transform.position;
        }
    }
}
