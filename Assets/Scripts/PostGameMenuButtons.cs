using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PostGameMenuButtons : MonoBehaviour
    {
        public void ConfirmReturnToMainMenu()
        {
            Debug.Log("Returning to main menu");
            GameManager.Instance.ResetManagers();
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        public void ConfirmRestartLevel()
        {
            Debug.Log("Restarting level");
            GameManager.Instance.ResetManagers();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void StartNextLevel()
        {
            GameManager.Instance.ResetManagers();
            GameManager.Instance.LoadNextLevel();
        }
    }
}