using DebugBehaviour.Runtime;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MousePainter : VerboseMonoBehaviour
{
    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor = Color.yellow;

    public float radius = 0.1f;
    public float strength = 1;
    public float hardness = 1;
    public float drawRange = 10f;

    private bool click = false;

    private void FixedUpdate()
    {
        if (click)
        {
            Paint();
        }
    }

    private void Paint()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, drawRange))
        {
            DrawRay(ray.origin, hit.point - ray.origin, paintColor);
            if (hit.collider.TryGetComponent<Paintable>(out var p))
            {
                PaintManager.Instance.Paint(p, hit.point, radius, hardness, strength, paintColor);
            }
            else
            {
                PaintManager.Instance.MakePaintable(hit.collider);
            }
        }
    }

    public void OnPaint(CallbackContext context)
    {
        Debug.Log("Painting");
        if (context.performed == true)
        {
            OnPaintButtonPressed();
        }
        else if (context.canceled == true)
        {
            OnPaintButtonReleased();
        }
    }

    public void OnPaintButtonPressed()
    {
        if (mouseSingleClick)
        {
            Log("SingleClickPainting");
            Paint();
        }
        else
        {
            click = true;
        }
    }

    public void OnPaintButtonReleased()
    {
        click = false;
    }
}
