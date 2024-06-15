using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject treePrefab;
    public float minTreeDistance = 1f;
    public float maxTreeDistance = 3f;
    public int chunkLength = 10; // Length of a chunkData along the path
    public int chunkWidth = 10; // Width of a chunkData perpendicular to the path
    public float pathWidth = 2f; // Width of the clear path

    private Queue<GameObject> chunks = new Queue<GameObject>();
    private Vector3 nextChunkPosition;

    void Start()
    {
        // Initialize the first chunkData
        nextChunkPosition = transform.position;
        SpawnChunk();
    }

    void Update()
    {
        // Generate new chunkData if player is close to the end of the current chunkData
        if (Vector3.Distance(transform.position, nextChunkPosition) < maxTreeDistance * chunkLength)
        {
            SpawnChunk();
        }

        // Optional: Remove old chunks if they are far behind the player
    }

    private void SpawnChunk()
    {
        GameObject newChunk = new GameObject("ChunkData");

        for (int i = 0; i < chunkLength; i++)
        {
            for (int j = -chunkWidth / 2; j <= chunkWidth / 2; j++)
            {
                if (Mathf.Abs(j) > pathWidth / 2)
                {
                    Vector3 treePos = nextChunkPosition + new Vector3(j * maxTreeDistance, 0, i * maxTreeDistance);
                    SpawnTree(treePos, newChunk.transform);
                }
            }
        }

        // Update nextChunkPosition for the next chunkData
        nextChunkPosition += Vector3.forward * chunkLength * maxTreeDistance;

        // Add the new chunkData to the queue
        chunks.Enqueue(newChunk);

        // Optional: Remove oldest chunkData if there are too many
    }

    private void SpawnTree(Vector3 position, Transform parent)
    {
        Instantiate(treePrefab, position, Quaternion.identity, parent);
    }
}
