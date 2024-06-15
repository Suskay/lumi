using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSurvial()
    {
        SceneManager.LoadScene("Scenes/SurvivalMode/Chunks");
    }

    public void StartRace()
    {
        SceneManager.LoadScene("Scenes/RaceLevels/RaceLevelMenu");
    }

    public void EnterDebugMenu()
    {
        SceneManager.LoadScene("Scenes/DebugMenu/DebugMenu");
    }
}
