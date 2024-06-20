using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class ChunkGenerator : MonoBehaviour
{
    public static ChunkGenerator Instance { get; private set; } // Singleton, but i didnt implement the singleton pattern fully, i.e. no destroy method if new instance is created
    public Vector3 startPosition = new Vector3(0, 1.85f, 0);
    public float spawnThreshold = 50f;
    public float removeThreshold = 50f; 
    public List<ChunkData> ChunkDatas; //all the chunks from ChunkStorage
    public List<ChunkData> CheckpointChunkDatas;
    public List<Chunk> activeChunks = new List<Chunk>(); // chunks currently in the scene
    public Chunk lastChunk;
    private FollowShadow followShadowScript;
    private int count = 0;
    public delegate void ChunkGeneratedEventHandler(Vector3 newChunkCenter);
    public event ChunkGeneratedEventHandler ChunkGenerated;
    private void OnChunkGenerated(Vector3 newChunkCenter)
    {
        Debug.Log(3);
        
        ChunkGenerated?.Invoke(newChunkCenter);
    }
    private void Awake()
    {
        Debug.Log(1);
        if (Instance != null)
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        GameObject lumiObject = GameObject.Find("Lumi");
        if (lumiObject != null)
        {
            followShadowScript = lumiObject.GetComponent<FollowShadow>();
            if (followShadowScript == null)
            {
                Debug.LogError("FollowShadow script not found on Lumi object.");
            }
        }
        else
        {
            Debug.LogError("Lumi object not found in the scene.");
        }
        
        ChunkDatas = ChunksStorage.Instance.ChunkDataList;
        CheckpointChunkDatas = ChunksStorage.Instance.CheckpointChunkDataList;
        activeChunks = new List<Chunk>();
        Chunk firstChunk = new Chunk(ChunkDatas[0], startPosition);
        lastChunk = firstChunk;
        firstChunk.spawnChunk();
        /*
        spawnNextChunk(false);
        spawnNextChunk(false);
        spawnNextChunk(false);
        spawnNextChunk(true);
        */
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current tree position
        Vector3 currentTreePosition = followShadowScript.currentShadowTransform.position;
        
        
       if(Input.GetKeyUp(KeyCode.Space))
        {
            if (Vector3.Distance(currentTreePosition, lastChunk.globalCenter) < spawnThreshold)
            {
                spawnNextChunk(count > 1);
                count = (count + 1) % 3;
            }

            // Check the distance to the first chunk
            if (activeChunks.Count > 0 &&
                Vector3.Distance(currentTreePosition, activeChunks[0].globalCenter) > removeThreshold)
            {
                removeChunk();
            }
        }
        
        
        
    }

    //Instantiates the next chunk randomly, adds it into the activeChunks list
    //Will have infinite loop if not at least one chunk for direction, but not to worry, there are always chunks for all directions
    private void spawnNextChunk(bool hasCheckpoint)
    {
        TreeData lastChunkConnectionTree = getChunkConnectionTree();
        ChunkData nextChunk = null;
        TreeData nextChunkConnectionTree = null;
        //loop until a chunk with a suitable connection tree is found
        for (int i = 0; nextChunkConnectionTree == null; i++)
        {
            if (hasCheckpoint)
            {
                nextChunk = CheckpointChunkDatas[Random.Range(0, CheckpointChunkDatas.Count)];
            }
            else
            {
                nextChunk = ChunkDatas[Random.Range(0, ChunkDatas.Count)];
            }
            nextChunkConnectionTree = getChunkConnectionTree(nextChunk, lastChunkConnectionTree);
        }
        //default option if no suitable connection tree is found
        if (nextChunkConnectionTree == null)
        {
            nextChunkConnectionTree = getChunkConnectionTree(ChunkDatas[0], lastChunkConnectionTree);
        }
        //Randomise position a bit
        float randomOffset = Random.Range(1.2f, 1.3f);
        Vector3 nextPosition = lastChunk.globalCenter + lastChunkConnectionTree.position - (nextChunkConnectionTree.position*randomOffset); // not correct yet
        
        Chunk newChunk = new Chunk(nextChunk, nextPosition);
        newChunk.spawnChunk();
        newChunk.availableDirections[nextChunkConnectionTree.connectionDirection] = false;
        lastChunk = newChunk;
        OnChunkGenerated(nextPosition);
        
    }
    
    //Returns a TreeData of potential connectionTree with a connectionDirection that is free,
    //to be used for connection to the next chunkData
    // null if no such tree exists
    private TreeData getChunkConnectionTree()
    {
        ChunkData lastChunkData = lastChunk.chunkData;
        // Gather all the connection trees from the lastChunkData with unused connection directions
        TreeData connectionTreeData;
        int availableDirection;
        for(int c  = 0; c < 10; c++)
        {
            int i = Random.Range(0, 4);
            int iLast = lastChunkData.ConnectionTreeDataList[i].Count-1;
            if (iLast != -1)
            {
                connectionTreeData = lastChunkData.ConnectionTreeDataList[i][Random.Range(0, iLast)];
                availableDirection = connectionTreeData.connectionDirection;
                if(lastChunk.availableDirections[availableDirection])
                {
                    return connectionTreeData;
                }
            }
        }
        Debug.LogError("No suitable connection tree found in last chunk");
        return null;
    }
    
    //Returns a TreeData of potential connectionTree with a connectionDirection that is free for toBeConnectedTree
    private TreeData getChunkConnectionTree(ChunkData chunkData, TreeData toBeConnectedTree)
    {
        // Gather all the connection trees from the lastChunkData with unused connection directions
        TreeData connectionTreeData = null;
        int availableDirection;
        for(int c  = 0; c < 10; c++)
        {
            
            int iLast = chunkData.ConnectionTreeDataList[(toBeConnectedTree.connectionDirection + 2) % 4].Count-1;
            if (iLast != -1)
            {
                
                connectionTreeData = chunkData.ConnectionTreeDataList[(toBeConnectedTree.connectionDirection + 2) % 4][Random.Range(0, iLast)];
                availableDirection = connectionTreeData.connectionDirection;
                    return connectionTreeData;
            }
            
            availableDirection = -1;
            connectionTreeData = null;
        }
        //Debug.Log("No suitable connection tree found in selected chunk for direction: " + (toBeConnectedTree.connectionDirection+2)%4);
        return null;
    }
    
    //Removes and destroys the first chunk from the activeChunks list
    private void removeChunk()
    {
            Chunk chunk = activeChunks[0];
            activeChunks.RemoveAt(0);

            List<GameObject> treesToRemove = new List<GameObject>();
            foreach (GameObject tree in chunk.trees)
            {
                treesToRemove.Add(tree);
            }

            foreach (GameObject tree in treesToRemove)
            {
                chunk.trees.Remove(tree);
                Destroy(tree);
            }
            Debug.Log("chunk before Destroy: " + chunk.globalCenter);
            
        
    }
    
    public class Chunk
    {
        public ChunkData chunkData;
        public List<GameObject> trees;
        public bool[] availableDirections = {false, true, true, true}; // 0 = south, 1 = west, 2 = north, 3 = east
        public Vector3 globalCenter;
        public Chunk(ChunkData chunkData, Vector3 globalCenter)
        {
            this.chunkData = chunkData;
            this.globalCenter = globalCenter;
            trees = new List<GameObject>();
        }
        
        // Spawns the chunk at the given center with the given chunkData
        public Chunk spawnChunk()
        {
            Vector3 chunkCenter = globalCenter;
            Instance.activeChunks.Add(this);
            foreach (TreeData tree in chunkData.TreeDataList)
            {
                // Add the chunkData's center to the tree's local position to get its global position
                Vector3 globalTreePosition = chunkCenter + tree.position;
                switch (tree.treeType)
                {
                    case 0:
                        trees.Add(Instantiate(TreeStorage.Instance.DefaultTree, globalTreePosition, Quaternion.identity));
                        break;
                    case 1:
                        trees.Add(Instantiate(TreeStorage.Instance.BirchTree, globalTreePosition, Quaternion.identity));
                        break;
                    case 2:
                        trees.Add(Instantiate(TreeStorage.Instance.CheckpointTree, globalTreePosition, Quaternion.identity));
                        break;
                
                }
            }

            return this;
        } 
    }
}
