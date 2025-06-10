using UnityEngine;

public class ScoreSyncedScriptable : MonoBehaviour
{
    public SyncedValue<int> currentScore;
    public bool m_autoReset;

    public void IncreaseScore(int score)
    {
        currentScore.SourceValue += score;
    }

    public void ResetScore()
    {
        currentScore.SourceValue = 0;
    }

    private void OnEnable()
    {
        if (m_autoReset)
        {
            ResetScore();
        }
    }
}
