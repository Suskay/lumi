using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{/*
    public GameObject treePrefab; // The combined Tree prefab to be instantiated
    public float treeThreshold = 0.5f;     // Threshold for tree placement
    public float generationDistance = 1f;
    public float density = 2f;
    void Start()
    {
        Debug.Log("Generate trees");
        GenerateTrees();
    }

    void GenerateTrees()
    {
        // Find the existing plane in the scene by name
        GameObject flatGround = GameObject.Find("FlatGround");

        // Check if the plane exists in the scene
        if (flatGround == null)
        {
            Debug.LogError("Flat ground not found in the scene. Make sure the plane is named 'FlatGround'.");
            return;
        }

        // Calculate scaled dimensions of the flat ground
        float scaledWidth = flatGround.transform.localScale.x;
        float scaledHeight = flatGround.transform.localScale.z;

        // Set a seed for Perlin noise
        int seed = (int)System.DateTime.Now.Ticks;
        UnityEngine.Random.InitState(seed);

        // Loop through the scaled flat ground to determine tree placement
        for (float x = flatGround.transform.position.x - scaledWidth / 2f;
             x < flatGround.transform.position.x + scaledWidth / 2f;
             x += density)
        {
            for (float z = flatGround.transform.position.z - scaledHeight / 2f;
                 z < flatGround.transform.position.z + scaledHeight / 2f;
                 z += density)
            {
                Debug.Log("Inside Perlin Generation");
                // Simulate terrain height using Perlin noise
                float height = Mathf.PerlinNoise(x * 0.1f, z * 0.1f);

                // Check if the height is above the threshold for tree placement
                if (height > treeThreshold)
                {
                    // Calculate the distance from the camera
                    float distanceToCamera = Vector3.Distance(new Vector3(x, 0f, z), Camera.main.transform.position);

                    // Check if the tree is within the generation distance
                    if (distanceToCamera <= generationDistance)
                    {
                        // Instantiate a new tree prefab at this position
                        Debug.Log("Creating a tree");
                        GameObject newTree = Instantiate(treePrefab, new Vector3(x, height * 10f, z), Quaternion.identity);

                        // Optional: Attach additional scripts to the instantiated tree object if needed
                        // newTree.AddComponent<YourAdditionalScript>();
                    }
                }
            }
        }
    }
    */
}

