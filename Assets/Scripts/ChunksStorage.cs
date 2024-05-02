using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksStorage : MonoBehaviour
{

    public static List<Chunk> Chunks;

    void Start()
    {
        Chunks = new List<Chunk>();
        createChunks(Chunks);
        scanChunks(Chunks);
        
    }
    // Scans and adds all Tree game objects on the Scene to the chunks 
    private void scanChunks(List<Chunk> chunkList)
    {
        GameObject[] allTrees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject tree in allTrees)
        {
            Vector3 treePosition = tree.transform.position;
            foreach (Chunk chunk in chunkList)
            {
                if(treePosition.x > chunk.xLowerBound.x && treePosition.x < chunk.xUpperBound.x &&
                   treePosition.z > chunk.zLowerBound.z && treePosition.z < chunk.zUpperBound.z)
                { 
                    chunk.addTree(tree);
                    Debug.Log("tree added to chunk:" +chunk.globalCenter);
                    break;
                }   
            }
        }
    }
//Creates empty chunks and adds them to the list
    private void createChunks(List<Chunk> chunkList)
    {
        float distance = ChunkBoundariesGizmos.chunkDistance;
        float size = ChunkBoundariesGizmos.chunkWidth;
        float xCoord = -15f, zCoord = -15f;
        for (float x = 0f; x < 2f; x++) // 2 chunks in x direction
        {
            for (float z = 0f; z < 1f; z++) // 1 chunk in z direction
            {
                chunkList.Add(new Chunk(xCoord + x*distance, xCoord + size + x*distance, zCoord + z*distance, zCoord + size + z*distance,
                    new Vector3(xCoord +size/2 + x*distance, 1.85f, zCoord + size/2 + z*distance)));
                Debug.Log("chunk created:" +x+ " "+z);
            }
        }
    }
}



public class Chunk : MonoBehaviour
{
    public Vector3 globalCenter; // Center of the chunk in global coordinates
    public float y = 1.85f;
    public float connectionDistance = 2f;
    public List<GameObject> trees = new List<GameObject>();
    public List<GameObject> connectionTrees = new List<GameObject>();
    public Vector3 xLowerBound, xUpperBound, zLowerBound, zUpperBound; // Boundaries of the chunk

    public Chunk(float x1, float x2, float z1, float z2, Vector3 globalCenter)
    {
        xLowerBound = new Vector3(x1, y, 0);
        xUpperBound = new Vector3(x2, y, 0);
        zLowerBound = new Vector3(0, y, z1);
        zUpperBound = new Vector3(0, y, z2);
        this.globalCenter = globalCenter;
    }
        
    // Add a tree to the chunk, return true if the tree is added successfully,
    // false if the tree is already in the chunk
    public bool addTree(GameObject tree)
    {

        GameObject localCoordinatesTree = Instantiate(tree);
            localCoordinatesTree.transform.position = new Vector3(tree.transform.position.x - globalCenter.x,
                y, tree.transform.position.z - globalCenter.z);
        if (trees.Contains(localCoordinatesTree))
        {
            return false;
        }
        trees.Add(localCoordinatesTree);
        if (checkConnection(tree))
        {
            connectionTrees.Add(localCoordinatesTree) ;
            print("connection added to chunk: " +globalCenter);
        } 
        return true;
    }
    //Check if the tree is close to the boundary of the chunk, return true if it is
    private bool checkConnection(GameObject tree)
    {
        TreeGenConnection connection = tree.GetComponent("TreeGenConnection") as TreeGenConnection;
        if (connection == null) return false; // if the tree does not have a connection component
        if (connection.connectionDirection != -1)
        {
            return true;
        }
        return false;
    }
}
