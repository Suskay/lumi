using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChunksStorage : MonoBehaviour
{
    public static ChunksStorage Instance { get; private set; }
    public List<ChunkData> ChunkDataList;
    public List<ChunkData> CheckpointChunkDataList;
    public int rows = 3; // rows of chunks
    public int columns = 3; // columns of chunks
    public Vector3 [] globalChunkCenters; // array of global chunk centers
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); 
            // This line prevents the object from being destroyed when a new scene is loaded
        }
    }
    void Start()
    {
        ChunkDataList = new List<ChunkData>();
        createChunks(ChunkDataList);
        scanChunks(ChunkDataList);
        separateCheckpointChunks();
        SceneManager.LoadScene("Scenes/SurvivalMode/Survival");
        
    }
    // Scans and adds all Tree game objects on the Scene to the chunks 
    private void scanChunks(List<ChunkData> chunkList)
    {
        
        ChunkData chunkData = new ChunkData();
        foreach (GameObject tree in TreeStorage.Instance.AllTrees)
        {
            Vector3 treePosition = tree.transform.position;
            for (int i = 0; i < globalChunkCenters.Length; i++)
            {   Vector3 globalCenter = globalChunkCenters[i];
                float xLowerBound = globalCenter.x - ChunkData.size / 2;
                float xUpperBound = globalCenter.x + ChunkData.size / 2;
                float zLowerBound = globalCenter.z - ChunkData.size / 2;
                float zUpperBound = globalCenter.z + ChunkData.size / 2;
                
                if(treePosition.x > xLowerBound && treePosition.x < xUpperBound &&
                   treePosition.z > zLowerBound && treePosition.z < zUpperBound)
                { 
                    ChunkDataList[i].addTree(tree, globalCenter);
                    break;
                }   
            }
        }
    }
    private void separateCheckpointChunks()
    {
        CheckpointChunkDataList = new List<ChunkData>();
        foreach (ChunkData chunkData in ChunkDataList)
        {
            if (chunkData.hasCheckpoint)
            {
                CheckpointChunkDataList.Add(chunkData);
            }
        }

        foreach (ChunkData chunkData in CheckpointChunkDataList)
        {
            ChunkDataList.Remove(chunkData);
        }
    }
//Creates empty ChunkDataList with as many chunks as cells in the globalChunkCenters
//Populates globalChunkCenters with global coordinates of chunk centers
    private void createChunks(List<ChunkData> chunkList)
    {
        float totalChunkDistance = ChunkBoundariesGizmos.totalChunkDistance;
        globalChunkCenters = new Vector3[rows*columns];
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < columns; z++)
            {
                chunkList.Add(new ChunkData());
                globalChunkCenters[x * rows + z] =  new Vector3(x * totalChunkDistance, ChunkData.Y, z * totalChunkDistance);
            }
        }
    }
}


public class TreeData // stores all essential information about trees
{
    public Vector3 position;
    public int connectionDirection;
    public int treeType; // 0 - default, 1 - birch, 2 - checkpoint
    
    public TreeData(Vector3 position, int connectionDirection, int treeType)
    {
        this.position = position;
        this.connectionDirection = connectionDirection;
        this.treeType = treeType;
    }
}

public class ChunkData
{
    
    public const float Y = 1.85f;
    public List<TreeData> TreeDataList;
    public List<TreeData>[] ConnectionTreeDataList; // each list corresponds to a direction of connection
    public const float size = 30f; // length of a side of the chunk square
    public bool hasCheckpoint = false;

    public ChunkData()
    {
        TreeDataList = new List<TreeData>();
        ConnectionTreeDataList = new List<TreeData>[4];
        for (int i = 0; i < 4; i++)
        {
            ConnectionTreeDataList[i] = new List<TreeData>();
        }
    }


    
    // takes as a parameter scanned tree  and coordinates of the scanned chunk center
    // Adds information about tree to the chunkData
    public void addTree(GameObject tree, Vector3 globalCenter)
    {   
        if(tree == null)
        {
            Debug.Log("tree is null");
        }
        if(tree.GetComponent<CheckpointTree>()!= null)
        {
            hasCheckpoint = true;
        }
        TreeGenConnection treeGenConnection = tree.GetComponent<TreeGenConnection>();
        TreeData localCoordinatesTreeData = new TreeData(tree.transform.position - globalCenter,
                                                                treeGenConnection.connectionDirection, treeGenConnection.treeType);
        if(TreeDataList == null)
        {
            Debug.Log("TreeDataList is null");
        }
        TreeDataList.Add(localCoordinatesTreeData);
        addPossibleConnection(localCoordinatesTreeData, globalCenter);
       
        
    }
    //Adds tree to a corresponding ConnectionTreeDataList if applicable
    private void addPossibleConnection(TreeData tree, Vector3 globalCenter)
    {
        if(tree.connectionDirection != -1)
        {
            ConnectionTreeDataList[tree.connectionDirection].Add(tree);
           
        }
    }
}
