using UnityEngine;

[RequireComponent(typeof(GhostRecorder))]
[RequireComponent(typeof(GhostPlayer))]
[RequireComponent(typeof(GhostRewinder))]
public class GhostManager : MonoBehaviour
{
    #region Publics
    public GhostModeEnum m_mode;
    #endregion


    #region Unity Api

    private void OnEnable()
    {
        m_player = GetComponent<GhostPlayer>();
        m_recorder = GetComponent<GhostRecorder>();
        m_rewinder = GetComponent<GhostRewinder>();
        m_player.enabled = false;
        m_recorder.enabled = false;
        m_rewinder.enabled = false;
        Record();
    }
    #endregion


    #region Main Methods
    public void Play()
    {
        m_mode = GhostModeEnum.Play;
        m_player.enabled = true;
        m_player.m_time = 0;
        m_recorder.enabled = false;
        m_rewinder.enabled = false;
    }

    public void Record()
    {
        m_mode = GhostModeEnum.Record;
        m_player.enabled = false;
        m_recorder.enabled = true;
        m_rewinder.enabled = false;
    }

    public void Rewind()
    {
        m_mode = GhostModeEnum.Rewind;
        m_player.enabled = false;
        m_recorder.enabled = false;
        m_rewinder.enabled = true;
    }
    #endregion


    #region Utils

    #endregion


    #region Private and Protected
    private GhostPlayer m_player;
    private GhostRecorder m_recorder;
    private GhostRewinder m_rewinder;
    #endregion


}
