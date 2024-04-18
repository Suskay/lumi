using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            StartCoroutine(PlayThemeSongWhenReady());
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        if (gameObject.tag == "Survival")
        {
            SoundManager.Instance.PlayGameOverSound();
        }
        else if (gameObject.tag == "Race")
        {
            
        }
        // Display game over UI here if not done elsewhere
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        IsGameOver = false;
        SoundManager.Instance.PlayThemeSong();
        // Reset any other necessary states or values here
    }

    private void Update()
    {
        if (IsGameOver)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
            {
                RestartGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the name of your main menu scene
            }
        }
    }

    private IEnumerator PlayThemeSongWhenReady()
        {
            yield return new WaitUntil(() => SoundManager.Instance != null);
            SoundManager.Instance.PlayThemeSong();
        }
}
