using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateTrees : MonoBehaviour
{
    public Vector3 startPoint = new Vector3(0f,0f,0f);
    public Vector3 endPoint = new Vector3(10f,0f,0f);
    public Vector3 controlPoint1= new Vector3(5f,0f,10f);
    public Vector3 controlPoint2= new Vector3(7f,0f,-2f);

    public int resolution = 20; // Number of points along the curve

    void OnDrawGizmos()
    {
        DrawBezierCurve();
    }

    void DrawBezierCurve()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 point = CalculateBezierPoint(t, startPoint, endPoint, controlPoint1, controlPoint2);
            Gizmos.DrawSphere(point, 0.1f);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // (1 - t)^3 * P0
        p += 3 * uu * t * p1; // 3(1 - t)^2 * t * P1
        p += 3 * u * tt * p2; // 3(1 - t) * t^2 * P2
        p += ttt * p3; // t^3 * P3

        return p;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    /*
    public static Vector3 position;
    public static bool[,,] grid; //first [] 
    public float noiseScale = 4f; //theoretically larger number means less smoothness
    public float density = 0.12f; //larger number => more trees
    public GameObject[] treePrefabs; 
    public int maxDistance = 2; // I need it to calculate if trees can be reached, sort of proportional to size of the tree
    public int sizeX = 20;
    public int sizeZ = 100;

    public int chunkNumber = 3; //how many chunks will be loaded at the same time
    
    void Start()
    {
        // position = FollowShadow.currentPosition; // to be used later for deleting far away chunks
        grid = new bool[chunkNumber, sizeX, sizeZ];
        generateTrees(0); // create the first chunk of trees
    }

    
    void Update()
    {
        //The idea is to create new chunks as player moves forward, deleting the previous ones
        //With checkConnections we will be able to make a continuous route
        //First values for curX and curZ are the Start tree coordinates
        //there is Destroy function to clean up and in the grid we remember the location of trees
        // if we make grid into float or GameObject we will be able to accomodate the fact that trees have different heights
        // but for now we have just one size so I made it as bool array 
    }

    private void generateTrees( int currentChunk)
    {
     bool allConnected = false;
        while (!allConnected) // when i fix the check trees, it will generate again if there is no possible route 
        {                     // to the end of the chunk
            bool[,] hasTreeArray = new bool[sizeX,sizeZ];
            
            for (int x = 0; x < sizeX; x++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    // use offsets as seeds, because perlin is deterministic
                    (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
                    float noiseValue = Mathf.PerlinNoise(x * noiseScale + xOffset, z * noiseScale + yOffset);
                    if (noiseValue < density)  hasTreeArray[x, z] = true; // mark field as occupied
                }
            }

            allConnected = true; //checkConnections(hasTreeArray, new bool[sizeX, sizeZ], maxDistance, 10, 0);
            if (allConnected)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    for (int z = 0; z < sizeZ; z++)
                    {
                        if (hasTreeArray[x, z])
                        {
                            //storing tree locations and actually creating them
                            grid[currentChunk, x, z] = hasTreeArray[x, z];
                            GameObject prefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
                            GameObject tree = Instantiate(prefab, transform);
                            tree.transform.position = new Vector3(x + Random.Range(0,0.5f), 1.848f, z + Random.Range(0,0.5f));
                        }
                    }
                }
            }
        }
    }

    //TODO: Currently buggy and not working, bugfix needed
    // checks if all trees are reachable
    private bool checkConnections(bool[,] hasTreeArray, bool[,] connectedTo, int radius, int curX, int curZ)
    {
        if(curZ >= sizeZ - 2) return true; //if we reached end of the chunk, then there is some connection 
        
        int xPosOffset = (curX + radius > sizeX ) ? sizeX - curX-1 : radius;
        int xNegOffset = (curX - radius < 0) ? radius - curX-1 : radius;
        int zPosOffset = (curZ + radius > sizeZ ) ? sizeZ - curZ-1 : radius;
        int zNegOffset = (curZ - radius < 0) ? radius - curZ-1 : radius; // to make sure we dont go out of bounds
        
        for (int x = curX - xNegOffset; x < curX + xPosOffset; x++)//not really a radius, 1/2 of a side of a search square
        {
            for (int z = curZ - zNegOffset; z < curZ + zPosOffset; z++)
            {
                if (hasTreeArray[x, z])
                {
                    if (connectedTo[x, z]) ;//if it is already in the list of connected trees, then we already checked it and do nothing
                    else
                    {
                        connectedTo[x, z] = hasTreeArray[x, z];
                        return checkConnections(hasTreeArray, connectedTo, radius, x, z); //check its surrounding trees
                    }
                }
            }
        }
        return false;
    }
*/}
