using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SyncedBoolValue m_gameIsOver;
    [SerializeField]
    private SyncedBoolValue m_playerIsAlive;


    private void Start()
    {
        m_gameIsOver.SourceValue = false;
        m_playerIsAlive.SourceValue = true;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        m_gameIsOver.SourceValue = true;
    }
    public void OnPlayButtonPushed()
    {

        SceneManager.LoadScene("DesignScene", LoadSceneMode.Single);
    }
    public void OnQuitButtonPushed()
    {
        Application.Quit();
    }

    public void OnPlayerAlive(bool isAlive)
    {
        if (!isAlive)
        {
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
            enabled = false;
        }
    }
}
