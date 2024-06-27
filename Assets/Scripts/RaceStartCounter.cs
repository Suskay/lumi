using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceStartCounter : MonoBehaviour
{
    public Text countdownText; // Assign in Inspector
    public static bool isRaceStarted = false;

    private void Start()
    {
        // Find the CountdownText
        countdownText = GameObject.Find("RaceStartCounter").GetComponent<Text>();
        
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        isRaceStarted = false;
        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        countdownText.text = "GO";
        yield return new WaitForSeconds(1);

        countdownText.enabled = false;
        isRaceStarted = true;
    }
}