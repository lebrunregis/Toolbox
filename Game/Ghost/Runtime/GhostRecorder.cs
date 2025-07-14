using UnityEngine;

[RequireComponent(typeof(GhostTimeline))]
public class GhostRecorder : MonoBehaviour
{
    #region Public

    public float m_recordDelta = 0;
    public float m_time = 0;
    #endregion

    public void OnEnable()
    {
        m_timeline = GetComponent<GhostTimeline>();
        m_recordDelta = m_timeline.m_timeScale;
        m_time = 0;
    }

    public void Update()
    {
        m_time += Time.deltaTime;
        m_recordDelta -= Time.deltaTime;
        if (m_recordDelta <= 0)
        {
            m_recordDelta = m_timeline.m_timeScale;
            GhostRecord ghostRecord = new(m_time, transform.position, transform.rotation);
            AddRecord(ghostRecord);
        }
    }

    public void AddRecord(GhostRecord record)
    {
        Debug.Log("Frame recorded", this);
        m_timeline.m_records.AddLast(record);
    }


    #region Private
    private GhostTimeline m_timeline;
    #endregion
}
