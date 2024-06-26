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
    public AudioSource jumpSound1;
    public AudioSource jumpSound2;
    public AudioSource jumpSound3;
    public AudioSource jumpSound4;
    public AudioSource jumpSound5;
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

    public void PlayJumpSound()
    {
        int random = Random.Range(1, 6);
        switch (random)
        {
            case 1:
                jumpSound1.Play();
                break;
            case 2:
                jumpSound2.Play();
                break;
            case 3:
                jumpSound3.Play();
                break;
            case 4:
                jumpSound4.Play();
                break;
            case 5:
                jumpSound5.Play();
                break;
        }
    }
}