using UnityEngine;

public class CheckpointTree : MonoBehaviour
{
    public bool isUsed = false; // Flag to check if the checkpoint tree has been used
    public ParticleSystem checkpointParticles;  // Reference to the particle system

    // turn isUsed to true and play the particle system
    public void UseTree()
    {
        isUsed = true;
        SoundManager.Instance.PlayCheckpointSound();
        checkpointParticles.Play();
    }


}