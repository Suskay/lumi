using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject treePrefab;
    public float minTreeDistance = 1f;
    public float maxTreeDistance = 3f;
    public int chunkLength = 10; // Length of a chunk along the path
    public int chunkWidth = 10; // Width of a chunk perpendicular to the path
    public float pathWidth = 2f; // Width of the clear path

    private Queue<GameObject> chunks = new Queue<GameObject>();
    private Vector3 nextChunkPosition;

    void Start()
    {
        // Initialize the first chunk
        nextChunkPosition = transform.position;
        SpawnChunk();
    }

    void Update()
    {
        // Generate new chunk if player is close to the end of the current chunk
        if (Vector3.Distance(transform.position, nextChunkPosition) < maxTreeDistance * chunkLength)
        {
            SpawnChunk();
        }

        // Optional: Remove old chunks if they are far behind the player
    }

    private void SpawnChunk()
    {
        GameObject newChunk = new GameObject("Chunk");

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

        // Update nextChunkPosition for the next chunk
        nextChunkPosition += Vector3.forward * chunkLength * maxTreeDistance;

        // Add the new chunk to the queue
        chunks.Enqueue(newChunk);

        // Optional: Remove oldest chunk if there are too many
    }

    private void SpawnTree(Vector3 position, Transform parent)
    {
        Instantiate(treePrefab, position, Quaternion.identity, parent);
    }
}
