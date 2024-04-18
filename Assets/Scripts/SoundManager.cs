using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource boostActivatedSound1;
    public AudioSource boostActivatedSound2;
    public AudioSource gameOverSound;
    public AudioSource unsuccessfulJumpSound;
    public AudioSource themeSong;
    public AudioSource checkpoint;

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

    public void PlayBoostActivatedSound()
    {
        if (UnityEngine.Random.value < 0.5f)
            {
                boostActivatedSound1.Play();
            }
            else
            {
                boostActivatedSound2.Play();
            }
    }

    public void PlayGameOverSound()
    {
        gameOverSound.Play();
    }

    public void PlayUnsuccessfulJumpSound()
    {
        unsuccessfulJumpSound.Play();
    }

    public void PlayCheckpointSound()
    {
        checkpoint.Play();
    }

    public void PlayThemeSong()
    {
        if (themeSong != null)
        {
            themeSong.Play();
        }
        else
        {
            Debug.LogError("Theme song AudioSource is not assigned in the Unity editor.");
        }
    }

    public void PlayEndTreeSound()
    {
        // TODO: play the end sound
        checkpoint.Play();
    }
}