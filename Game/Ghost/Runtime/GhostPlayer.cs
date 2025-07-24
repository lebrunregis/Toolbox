using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

[RequireComponent(typeof(GhostTimeline))]
public class GhostPlayer : MonoBehaviour
{
    #region Public
    public float m_time = 0;
    public float m_timeScale = 1;
    private Tween m_positionTween;
    private Tween m_rotationTween;
    #endregion

    #region Private
    private GhostTimeline m_timeline;
    private LinkedListNode<GhostRecord> m_currentNode;
    #endregion

    #region API
    private void OnEnable()
    {
        m_time = 0;
        m_timeline = GetComponent<GhostTimeline>();
        m_currentNode = m_timeline.m_records.First;
        m_positionTween = new Tween();
        m_rotationTween = new Tween();
        m_timeScale = m_timeline.m_timeScale;
    }
    private void Update()
    {
        if (m_currentNode != null)
        {
            m_time += Time.deltaTime;
            if (m_currentNode.Value.time <= m_time && !m_positionTween.isAlive)
            {
                float delta = m_time - m_currentNode.Value.time;
                Vector3 position = m_currentNode.Value.position;
                Quaternion rotation = m_currentNode.Value.rotation;

                if (transform.position != position)
                    m_positionTween = Tween.Position(transform, position, m_timeScale, Ease.Linear);
                if (transform.rotation != rotation)
                    m_rotationTween = Tween.Rotation(transform, rotation, m_timeScale, Ease.Linear);
                m_currentNode = m_currentNode.Next;

            }
        }
        else
        {
            enabled = false;
        }
    }
    #endregion
}
