using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public void ClearAllHighscores()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs data cleared");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}