using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStorage : MonoBehaviour
{
    public static TreeStorage Instance { get; private set; }
    public GameObject DefaultTree;
    public GameObject BirchTree;
    public GameObject CheckpointTree;
    public List<GameObject> AllTrees { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        AllTrees = new List<GameObject>();
    }

    public void RegisterTree(GameObject tree)
    {
        AllTrees.Add(tree);
    }

    public void UnregisterTree(GameObject tree)
    {
        AllTrees.Remove(tree);
    }
}
