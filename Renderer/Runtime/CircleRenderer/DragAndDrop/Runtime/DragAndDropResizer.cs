using Toolbox.Renderer.Runtime.CircleRenderer;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CircleRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class DragAndDropResizer : MonoBehaviour
{
    public Gradient draggedColor;
    public Gradient droppedColor;
    private bool dragging = false;
    private Camera cam;
    private LineRenderer lineRenderer;
    private Mesh mesh;
    private MeshCollider meshCollider;
    public float m_minScale = 1f;
    public float m_maxScale = 5f;

    public void BeginDrag()
    {
        lineRenderer.colorGradient = draggedColor;
        dragging = true;
    }

    public void EndDrag()
    {
        lineRenderer.colorGradient = droppedColor;
        dragging = false;
    }

    public void OnMouseDown()
    {
        Debug.Log("Drag");
        BeginDrag();
    }

    public void OnMouseUp()
    {
        Debug.Log("Drop");
        EndDrag();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cam = Camera.main;
        mesh = new Mesh();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.BakeMesh(mesh, true);
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    private void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = cam.nearClipPlane;
            Vector3 newPos = cam.ScreenToWorldPoint(mousePos);
            newPos.z = 0;
            float newScale = Vector3.Distance(transform.position, newPos);
            // Debug.Log(newPos.ToString() + " " + newScale.ToString());
            if (newScale < m_minScale)
            {
                newScale = m_minScale;
            }
            if (newScale > m_maxScale)
            {
                newScale = m_maxScale;
            }
            transform.parent.localScale = new Vector3(newScale, newScale, 1);
        }
    }
}
