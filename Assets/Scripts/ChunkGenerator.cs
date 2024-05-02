using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public Vector3 startPosition;
    public List<Chunk> chunks; //all the chunks from ChunkStorage
    public List<Chunk> activeChunks; // chunks currently in the scene
    public Chunk lastChunk;
    public float chunkRemovalDistance = 50f;
    void Start()
    {
        startPosition = GameObject.Find("StartingTree").transform.position + new Vector3(0, 0, -60);
        chunks = ChunksStorage.Chunks;
        Chunk newChunk = chunks[Random.Range(0, chunks.Count)];     
        InstantiateTreesFromChunk(GetNewChunkLocation(newChunk), newChunk);
        lastChunk = newChunk;
        activeChunks.Add(newChunk);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkRemoveChunks())
        {
            Destroy(activeChunks[0].gameObject);
            activeChunks.RemoveAt(0);
        }
    }
    public void InstantiateTreesFromChunk(Vector3 chunkCenter, Chunk chunk)
    {
        activeChunks.Add(chunk);
        foreach (GameObject tree in chunk.trees)
        {
            // Add the chunk's center to the tree's local position to get its global position
            Vector3 globalTreePosition = chunkCenter + tree.transform.position;
            Instantiate(tree, globalTreePosition, Quaternion.identity);
        }
    } 
    
    
    // determines the location where the next chunk should be placed
    public Vector3 GetNewChunkLocation(Chunk newChunk)
    {
        // Select a random connection point from the last chunk
        GameObject lastConnectionTree = lastChunk.connectionTrees[Random.Range(0, lastChunk.connectionTrees.Count)];
        TreeGenConnection lastConnection = lastConnectionTree.GetComponent<TreeGenConnection>();

        // Find a connection point in the new chunk with the same connection direction
        GameObject newConnectionTree = null;
        foreach (GameObject tree in newChunk.connectionTrees)
        {
            TreeGenConnection connection = tree.GetComponent<TreeGenConnection>();
            if (connection.connectionDirection == lastConnection.connectionDirection + 2 % 4)
            {
                newConnectionTree = tree;
                break;
            }
            {
                newConnectionTree = tree;
                break;
            }
        }

        // If no matching connection point was found, return the current chunk center
        if (newConnectionTree == null)
        {
            return lastChunk.globalCenter;
        }

        // Calculate the new chunk's center such that the distance between the connection points is less than connectionLength
        Vector3 connectionOffset = newConnectionTree.transform.position - lastConnectionTree.transform.position;
        Vector3 newChunkCenter = lastChunk.globalCenter + connectionOffset;

        // Ensure the distance between the connection points is less than connectionLength
        if (Vector3.Distance(lastConnectionTree.transform.position, newConnectionTree.transform.position) > 2)
        {
            newChunkCenter = lastChunk.globalCenter + (connectionOffset.normalized * 2);
        }

        return newChunkCenter;
    }

    //Checks if player has gotten to far ahead,
    // return true if distance to the last chunk is less than chunkRemovalDistance
    private bool checkRemoveChunks()
    {
        float distance = Vector3.Distance(GameObject.Find("Lumi").transform.position, lastChunk.globalCenter);
        if(distance < chunkRemovalDistance)
        {
            return true;
        }
        return false;
    }
    
}
