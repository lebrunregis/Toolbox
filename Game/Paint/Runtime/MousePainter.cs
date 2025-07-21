using DebugBehaviour.Runtime;
using UnityEngine;
namespace Paint.Runtime
{
    public class MousePainter : VerboseMonoBehaviour
    {
        public Camera cam;
        [Space]
        public bool mouseClicked;
        [Space]
        public Color paintColor;

        public float radius = 1;
        public float strength = 1;
        public float hardness = 1;
        public float maxDistance = 10;

        public void OnMouseDown()
        {
            mouseClicked = true;
        }

        public void OnMouseUp()
        {
            mouseClicked = false;
        }

        public void OnMouseOver()
        {
            if (mouseClicked == true)
            {
                Vector3 position = Input.mousePosition;
                Ray ray = cam.ScreenPointToRay(position);

                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
                {
                    DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                    transform.position = hit.point;
                    if (hit.collider.TryGetComponent<Paintable>(out Paintable p))
                    {
                        PaintManager.Instance.Paint(p, paintColor, hit.point, radius, hardness, strength);
                    }
                }
            }
        }
    }
}