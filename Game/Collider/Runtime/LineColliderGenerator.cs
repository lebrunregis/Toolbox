using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(LineRenderer))]
public class LineColliderGenerator : MonoBehaviour
{
    private new MeshCollider collider;
    private LineRenderer lineRenderer;
    private Mesh lineMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        lineMesh = GetComponent<Mesh>();
        lineRenderer = GetComponent<LineRenderer>();
        collider = GetComponent<MeshCollider>();
        collider.sharedMesh = lineMesh;
    }

    // Update is called once per frame
    private void Update()
    {

        lineRenderer.BakeMesh(lineMesh, true);

    }
}
