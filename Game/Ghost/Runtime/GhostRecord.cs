using UnityEngine;

public struct GhostRecord
{
    public float time;
    public Vector3 position;
    public Quaternion rotation;

    public GhostRecord(float time, Vector3 position, Quaternion rotation)
    {
        this.time = time;
        this.position = position;
        this.rotation = rotation;
    }
}
