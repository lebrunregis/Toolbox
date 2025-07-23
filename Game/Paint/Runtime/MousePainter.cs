using DebugBehaviour.Runtime;
using UnityEngine;

public class MousePainter : VerboseMonoBehaviour
{
    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;
    public float drawRange = 100f;

    private bool click = false;

    private void OnMouseOver()
    {
        if (click)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, drawRange))
            {
                DrawRay(ray.origin, hit.point - ray.origin, paintColor);
                if (hit.collider.TryGetComponent<Paintable>(out var p))
                {
                    PaintManager.Instance.Paint(p, hit.point, radius, hardness, strength, paintColor);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        click = true;
    }

    private void OnMouseUp()
    {
        click = false;
    }
}
