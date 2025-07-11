using System.Collections.Generic;
using UnityEngine;

public class GhostRewinder : MonoBehaviour
{
    #region Public

    public float m_time = 0;
    public LinkedListNode<GhostRecord> m_currentNode;
    #endregion

    public void OnEnable()
    {
        m_timeline = GetComponent<GhostTimeline>();
        if (m_timeline.count > 0)
        {
            m_time = m_timeline.m_records.Last.Value.time;
            m_currentNode = m_timeline.m_records.Last;
        }
        else
        {
            Debug.Log("Rewind enabled on an empty timeline.", this);
        }

    }

    public void Update()
    {
        m_time -= Time.deltaTime;
        if (m_currentNode != null)
        {

        }
        else
        {
            this.enabled = false;
        }
    }


    #region Private
    private GhostTimeline m_timeline;
    #endregion
}
