using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedInput : MonoBehaviour
{
    public static SeedInput Instance { get; private set; }
    public TMP_InputField seedInputField;
    public int seed = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void applySeed()
    {
        string seedStr = seedInputField.text;
        seed = seedStr.GetHashCode();
    }
    public int getSeed()
    {
        return seed;
    }
}
