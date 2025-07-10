using UnityEngine;


namespace Renderer.Runtime.CircleRenderer
{
    [RequireComponent(typeof(LineRenderer))]
    public class CircleRenderer : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        public int steps = 90;
        public float radius = 1f;
        public Gradient gradient = new Gradient();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.colorGradient = gradient;
            lineRenderer.loop = true;
            lineRenderer.useWorldSpace = false;
            DrawCircle(steps, radius);
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void DrawCircle(int steps, float radius)
        {
            lineRenderer.positionCount = steps;

            for (int i = 0; i < steps; i++)
            {
                float progress = (float)i / steps;

                float radians = progress * 2 * Mathf.PI;

                float x = Mathf.Cos(radians);
                float y = Mathf.Sin(radians);

                float xScaled = x * radius;
                float yScaled = y * radius;

                Vector2 pos = new Vector2(xScaled, yScaled);

                lineRenderer.SetPosition(i, pos);
            }
        }
    }

}
