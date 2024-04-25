using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBoundariesGizmos : MonoBehaviour
{
    
    public static float chunkDistance = 35f;
    public static float chunkHeight = 1f;
    public static float chunkWidth = 30f;
    public static float chunkDepth = 30f;
    public Color gizmoColor = Color.green;

    private void OnDrawGizmos()
    {
        for(int x = 0; x < 5; x++)
        {
            for(int z = 0; z < 5; z++)
            {
                Vector3 chunkPosition = new Vector3(x * chunkDistance, 0, z * chunkDistance);
                Gizmos.color = gizmoColor;
                Vector3 chunkSizeVector = new Vector3(chunkWidth, chunkHeight, chunkDepth);
                Gizmos.DrawWireCube(chunkPosition, chunkSizeVector);
            }
        }
        
    }
}
