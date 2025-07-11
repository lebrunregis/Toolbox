using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GhostTimeline))]
public class GhostTimeline : MonoBehaviour
{
    #region Public
    public float m_timeScale = 0.1f;
    public int count;
    public LinkedList<GhostRecord> m_records = new();
    #endregion

    private void Update()
    {
        count = m_records.Count;
    }
}
