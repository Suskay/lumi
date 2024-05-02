using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public Vector3 startPosition;
    public List<Chunk> chunks;
    public Chunk lastChunk;
    void Start()
    {
        startPosition = GameObject.Find("StartingTree").transform.position;
        chunks = ChunksStorage.Chunks;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateChunks()
    {
        int nextChunkInt = Random.Range(0, chunks.Count);
        Chunk nextChunk = chunks[nextChunkInt];
        
    }
    
    
    //Checks the connection points of the active chunk and the next chunk,
    // determines the location where the next chunk should be placed
    private Vector3 getNewChunkLocation()
    {
        return Vector3.zero;
    }
    
}
