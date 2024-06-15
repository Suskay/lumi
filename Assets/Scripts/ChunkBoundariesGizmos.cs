using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBoundariesGizmos : MonoBehaviour
{
    public static float chunkSideLength = ChunkData.size;
    public static float distanceBetweenChunks = 5f;
    public static float totalChunkDistance = 35f;
    public static float chunkHeight = 1f;
    public Color gizmoColor = Color.green;

    private void OnDrawGizmos()
    {
        for(int x = 0; x < 5; x++)
        {
            for(int z = 0; z < 5; z++)
            {
                Vector3 chunkPosition = new Vector3(x * totalChunkDistance, 0, z * totalChunkDistance);
                Gizmos.color = gizmoColor;
                Vector3 chunkSizeVector = new Vector3(chunkSideLength, chunkHeight, chunkSideLength);
                Gizmos.DrawWireCube(chunkPosition, chunkSizeVector);
            }
        }
        
    }
}
