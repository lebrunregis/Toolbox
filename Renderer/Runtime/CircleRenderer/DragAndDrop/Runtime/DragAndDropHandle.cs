using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DragAndDropHandle : MonoBehaviour
{
    public Color draggedColor = Color.green;
    public Color droppedColor = Color.red;
    private Camera cam;
    private SpriteRenderer spriteRenderer;
    public bool dragging;
    public Transform draggedTransform;

    public void BeginDrag()
    {
        spriteRenderer.color = draggedColor;
        dragging = true;
    }

    public void EndDrag()
    {
        spriteRenderer.color = droppedColor;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            draggedTransform.position = newPos;
        }
    }

}
