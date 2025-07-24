using DebugBehaviour.Runtime;
using UnityEngine;

public class CollisionPainter : VerboseMonoBehaviour
{
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.TryGetComponent<Paintable>(out var p))
        {
            Log("OnCollisionPainting");
            Vector3 pos = other.contacts[0].point;
            PaintManager.Instance.Paint(p, pos, radius, hardness, strength, paintColor);
        }
    }
}
