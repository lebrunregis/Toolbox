using UnityEngine;
using UnityEngine.Events;


public class SyncedUiScoreListener : SyncedValueListener<int>
{
    [SerializeField]
    public UnityEvent<string> m_raisedString;

    private void OnEnable()
    {
        int val = m_syncValue.Subscribe(m_raisedEvent);
        m_raisedEvent.AddListener(RaiseString);
        m_raisedEvent.Invoke(val);
        m_raisedString.Invoke(val.ToString());
    }

    private void RaiseString(int val)
    {
        m_raisedString.Invoke(val.ToString());
    }
}
