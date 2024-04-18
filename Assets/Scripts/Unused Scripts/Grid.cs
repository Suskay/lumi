using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *
 * This stuff is currently disabled, It is what i found on the internet when trying to figure out generation
 * It is not really suited for what we want to do, yet if we want something to do with terrain
 * it may be useful
 * 
 */
public class Grid : MonoBehaviour
{
    public GameObject[] treePrefabs;
    public float scale = .1f;
    public float treeLevel = .4f;
    public float treeNoiseScale = .05f;
    public float treeDensity = .2f;
    public int size = 100;
    Cell[,] grid;
    // public Vector3 position = FollowShadow.currentPosition;
    private void Start()
    {
        float[,] noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        float[,] falloffMap = new float[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                falloffMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }

        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = noiseMap[x, y];
                noiseValue -= falloffMap[x, y];
                bool isOccupied = noiseValue < treeLevel;
                Cell cell = new Cell(isOccupied);
                grid[x, y] = cell;
            }
        }
        Debug.Log("Call GenTrees");
        GenerateTrees(grid);
        
    }


    void GenerateTrees(Cell[,] grid)
    {
        Debug.Log("Inside GenTrees");
        float[,] noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * treeNoiseScale + xOffset, y * treeNoiseScale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }
        Debug.Log("Noise created");
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (!cell.isOccupied)
                {
                    Debug.Log("There is unoccupied cell");
                    float v = Random.Range(0f, treeDensity);
                    Debug.Log("Value of v: "+v);
                    if (noiseMap[x, y] < v)
                    {
                        GameObject prefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
                        GameObject tree = Instantiate(prefab, transform);
                        tree.transform.position = new Vector3(x, 1.848f, y);
                        //tree.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                        //tree.transform.localScale = Vector3.one * Random.Range(.8f, 2.2f);
                        Debug.Log("Tree created");
                       
                    }
                }
            }
        }
    }
    

}
