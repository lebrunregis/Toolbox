using UnityEngine;

namespace ScreenWrap
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ScreenWrapController : MonoBehaviour
    {
        public Camera m_camera;
        public ScreenBehaviour mode;
        private Rect screenRect;
        private Rigidbody2D rb;

        public enum ScreenBehaviour
        {
            None,
            Box,
            Bounce,
            XWrap,
            YWrap,
            Wrap,
            Cleanup
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            if (m_camera == null)
            {
                m_camera = Camera.main;
            }
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (m_camera != null)
            {
                screenRect.height = 2f * m_camera.orthographicSize;
                screenRect.width = screenRect.height * m_camera.aspect;
                screenRect.x = m_camera.transform.position.x - screenRect.width / 2;
                screenRect.y = m_camera.transform.position.y - screenRect.height / 2;

                switch (mode)
                {
                    case ScreenBehaviour.None:
                        break;
                    case ScreenBehaviour.Box:
                        this.transform.position = Box(transform.position, screenRect, rb);
                        break;
                    case ScreenBehaviour.XWrap:
                        this.transform.position = XWrap(transform.position, screenRect);
                        this.transform.position = Box(transform.position, screenRect, rb);
                        break;
                    case ScreenBehaviour.YWrap:
                        this.transform.position = YWrap(transform.position, screenRect);
                        this.transform.position = Box(transform.position, screenRect, rb);
                        break;
                    case ScreenBehaviour.Wrap:
                        this.transform.position = Wrap(transform.position, screenRect);
                        this.transform.position = Box(transform.position, screenRect, rb);
                        break;
                    case ScreenBehaviour.Cleanup:
                        Cleanup(transform.position, screenRect);
                        break;
                    case ScreenBehaviour.Bounce:
                        rb.linearVelocity = Bounce(transform.position, screenRect, rb.linearVelocity);
                        this.transform.position = Box(transform.position, screenRect, rb);
                        break;
                }
            }

        }

        private static Vector2 Bounce(Vector2 pos, Rect screenRect, Vector2 velocity)
        {
            if (pos.x < screenRect.x || pos.x > screenRect.x + screenRect.width)
            {
                velocity.x = -velocity.x;
            }

            if (pos.y < screenRect.y || pos.y > screenRect.y + screenRect.height)
            {
                velocity.y = -velocity.y;
            }

            return velocity;
        }

        private static Vector2 Box(Vector2 pos, Rect screenRect, Rigidbody2D rb)
        {
            return new Vector2(Mathf.Clamp(pos.x, screenRect.x, screenRect.x + screenRect.width),
                Mathf.Clamp(pos.y, screenRect.y, screenRect.y + screenRect.height));
        }

        private static bool IsOutsideView(Vector2 pos, Rect screenRect)
        {
            return IsOutsideBoundary(pos.x, screenRect.x, screenRect.x + screenRect.width) ||
                   IsOutsideBoundary(pos.y, screenRect.y, screenRect.y + screenRect.height);
        }

        private static float Wrap(float pos, float min, float max)
        {
            if (pos < min)
            {
                pos = max;
            }
            else if (pos > max)
            {
                pos = min;
            }

            return pos;
        }

        private static bool IsOutsideBoundary(float x, float minX, float maxX)
        {
            return x < minX || x > maxX;
        }

        private static Vector2 Wrap(Vector2 pos, Rect screenRect)
        {
            pos = YWrap(pos, screenRect);
            pos = XWrap(pos, screenRect);
            return pos;
        }

        private static Vector2 XWrap(Vector2 pos, Rect screenRect)
        {
            pos.x = Wrap(pos.x, screenRect.x, screenRect.x + screenRect.width);
            return pos;
        }

        private static Vector2 YWrap(Vector2 pos, Rect screenRect)
        {
            pos.y = Wrap(pos.y, screenRect.y, screenRect.y + screenRect.height);
            return pos;
        }

        private void Cleanup(Vector2 pos, Rect screenRect)
        {
            if (pos.x < screenRect.x - 2f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}