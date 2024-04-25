using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksStorage : MonoBehaviour
{
    
    public List<Chunk> Chunks { get; set; }

    void Start()
    {
        Chunks = new List<Chunk>();
        createChunks(Chunks);
        scanChunks(Chunks);
        
    }
    // Scans and adds all Tree game objects to the chunks 
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
                }
            }
        }
    }

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
            }
        }
    }
}



public class Chunk
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
        globalCenter = this.globalCenter;
    }
        
    // Add a tree to the chunk, return true if the tree is added successfully,
    // false if the tree is already in the chunk
    public bool addTree(GameObject tree)
    {
        //TODO: transform tree to the local coordinate system!!!
        //GameObject localCoordinatesTree = tree.transform.position();
        if (trees.Contains(tree))
        {
            return false;
        }
        trees.Add(tree);
        return true;
    }
    public bool addConnection(GameObject tree)
    {
        if (connectionTrees.Contains(tree))
        {
            return false;
        }
        connectionTrees.Add(tree);
        checkConnection(tree);
        return true;
    }

    public bool checkConnection(GameObject tree)
    {
        Vector3 treePosition = tree.transform.position;
        if (Vector3.Distance(treePosition, xLowerBound) < connectionDistance ||
            Vector3.Distance(treePosition, xUpperBound) < connectionDistance ||
            Vector3.Distance(treePosition, zLowerBound) < connectionDistance ||
            Vector3.Distance(treePosition, zUpperBound) < connectionDistance)
        {
             return addConnection(tree);
            
        }
        return false;
    }
}
