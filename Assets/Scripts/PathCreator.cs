using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    private ChunkGenerator chunkGenerator;
    private List<Vector3> pathPoints = new List<Vector3>();
    private Vector3 lastPosition = ChunkGenerator.Instance.startPosition;
    // Start is called before the first frame update
    void Start()
    {
        chunkGenerator = ChunkGenerator.Instance;
        chunkGenerator.ChunkGenerated += OnChunkGenerated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 point in pathPoints)
        {
            if (point.Equals(pathPoints[0]))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(point, 1f);
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.DrawSphere(point, 0.5f);
            }
            
        }
    }
    private void OnChunkGenerated(Vector3 newChunkCenter)
    {
        
        createSpline(lastPosition, newChunkCenter);
        Debug.Log("Spline created from " + lastPosition + " to " + newChunkCenter);
        lastPosition = newChunkCenter;
        
    }

    private void createSpline(Vector3 p1, Vector3 p2)
    {
        const float interval = 0.1f;
        float currentInterval = 0f;
        Vector3 midPoint = p1 + (p2-p1)/2 + new Vector3(50,0,0);
        while(currentInterval < 1)
        {
            Vector3 m1 = Vector3.Lerp(p1,midPoint,currentInterval);
            Vector3 m2 = Vector3.Lerp(midPoint,p2,currentInterval);
            Vector3 splinePoint = Vector3.Lerp(m1,m2,currentInterval);
            pathPoints.Add(splinePoint);
            currentInterval += interval;
        }
    }
    
    
    
}
