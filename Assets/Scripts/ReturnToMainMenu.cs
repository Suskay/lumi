using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMainMenu : MonoBehaviour
{
    public GameObject confirmationDialog; // Reference to the confirmation dialog UI element

    void Update()
    {
        // check if escape pressend and dialog is active
        if (Input.GetKeyDown(KeyCode.Escape) && !confirmationDialog.activeSelf && !GameManager.Instance.IsGameOver )
        {
            confirmationDialog.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && confirmationDialog.activeSelf)
        {
            CancelReturnToMainMenu();
        }
    }

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

    private void CancelReturnToMainMenu()
    {
        Debug.Log("Returning to game");
        confirmationDialog.SetActive(false);
    }
}