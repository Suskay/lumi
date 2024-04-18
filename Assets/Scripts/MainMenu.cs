using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSurvial()
    {
        // Load the game scene (replace "GameScene" with the name of your game scene)
        SceneManager.LoadScene("Scenes/Survival");
    }

    public void StartRace()
    {
        // Load the game scene (replace "GameScene" with the name of your game scene)
        SceneManager.LoadScene("Scenes/RaceLevels/Race1");
    }
}
