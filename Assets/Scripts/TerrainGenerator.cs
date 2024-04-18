using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        // Get the Terrain component attached to this GameObject
        Terrain terrain = GetComponent<Terrain>();
        // Generate the terrain based on Perlin noise
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // Set the heightmap resolution and size of the terrain
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, 10, height);
        // Set the heights of the terrain based on Perlin noise
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        // Generate Perlin noise to create terrain-like heights
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                heights[x, y] = sample;
            }
        }

        return heights;
    }
}

