using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class DynamicHitbox : MonoBehaviour
{
    private MeshCollider meshCollider;
    private Mesh combinedMesh;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        combinedMesh = new Mesh();
    }

    void Update()
    {
        CombineChildMeshes();
    }

    void CombineChildMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        combinedMesh.CombineMeshes(combine);
        meshCollider.sharedMesh = null; // Clear previous mesh to avoid issues
        meshCollider.sharedMesh = combinedMesh;
    }
}