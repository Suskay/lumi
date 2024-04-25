using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartSurvial()
    {
        SceneManager.LoadScene("Scenes/SurvivalMode/SurvivalMenu");
    }

    public void StartRace()
    {
        SceneManager.LoadScene("Scenes/RaceLevels/RaceLevelMenu");
    }
}
