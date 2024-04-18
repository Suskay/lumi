using UnityEngine;

public class EndTree : MonoBehaviour
{
    public bool isUsed = false; // Flag to check if the end tree has been used
    public ParticleSystem endTreeParticles;  // Reference to the particle system
    public TimerRaceMode timerRaceMode; // Reference to the TimerRaceMode script

    void Start()
    {
        // Find the TimerRaceMode script on the TimeManager GameObject
        timerRaceMode = GameObject.Find("TimeManager").GetComponent<TimerRaceMode>();
        if (timerRaceMode == null)
        {
            Debug.LogError("TimerRaceMode script not found on the TimeManager GameObject. Make sure it is active and the name is correct.");
        }
    }

    // turn isUsed to true, play the particle system, and stop the timer
    public void UseTree()
    {
        Debug.Log("Tree used!");
        isUsed = true;
        SoundManager.Instance.PlayEndTreeSound();
        endTreeParticles.Play();
        timerRaceMode.StopTimer();
    }
}