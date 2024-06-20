using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    private ChunkGenerator chunkGenerator;
    private List<Vector3> pathPoints = new List<Vector3>(); // exists for gizmos only
    private Vector3 lastPoint = ChunkGenerator.Instance.startPosition;
    private Vector3 midPoint;
    private Vector3 startPoint;
    private bool isEven;
    public GameObject footprint;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPoint = GameObject.Find("StartingTree").transform.position;
        chunkGenerator = ChunkGenerator.Instance;
        chunkGenerator.ChunkGenerated += OnChunkGenerated;
        isEven = true;

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
                Gizmos.DrawSphere(point, 0.5f);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPoint, 1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(midPoint, 1f);
        Gizmos.color = Color.yellow;
        Debug.Log("Last position is " + lastPoint);
        Gizmos.DrawSphere(lastPoint, 1f);
    }
    private void OnChunkGenerated(Vector3 newChunkCenter)
    {
        startPoint = midPoint;
        midPoint = lastPoint;
        lastPoint = newChunkCenter;
        isEven = !isEven;
        if (isEven)
        {
            instantiateFootprints(createSpline(startPoint, midPoint,lastPoint));
        }
        Debug.Log("Spline created from " + startPoint+ " to " + lastPoint);
    }

    private List<Vector3> createSpline(Vector3 start, Vector3 mid, Vector3 goal)
    {
        List<Vector3> pathPoints = new List<Vector3>();
        const float interval = 0.05f;
        float currentInterval = 0f;
        while(currentInterval < 1)
        {
            Vector3 m1 = Vector3.Lerp(start,mid,currentInterval);
            Vector3 m2 = Vector3.Lerp(mid,goal,currentInterval);
            Vector3 splinePoint = Vector3.Lerp(m1,m2,currentInterval);
            splinePoint = new Vector3(splinePoint.x, 0.1f, splinePoint.z);
            pathPoints.Add(splinePoint);
            currentInterval += interval;
        }
        this.pathPoints.AddRange(pathPoints);
        return pathPoints;
    }
// Instantiates footprints on the path generated with spline
    private void instantiateFootprints(List<Vector3> path)
    {
        Vector3 previousPoint = path[0];
        

        for (int i = 1; i < path.Count; i++)
        {
            Vector3 point = path[i];

            // Calculate the direction from the previous point to the current point
            Vector3 direction = (point - previousPoint).normalized;

            
            
            Quaternion rotation = Quaternion.FromToRotation(Vector3.left, direction);

            // Instantiate the footprint with the calculated rotation
            Instantiate(footprint, point, rotation);

            previousPoint = point;
        }
    }
    
}
