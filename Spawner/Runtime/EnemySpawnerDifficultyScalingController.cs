using Tools;
using UnityEngine;

[RequireComponent(typeof(EnemySpawnerController))]
[RequireComponent(typeof(Repeater))]
public class EnemySpawnerDifficultyScalingController : MonoBehaviour
{
    #region Publics
    public float m_time;
    public float m_timeScale = 1 / 60;
    public AnimationCurve m_spawnDelayAnimator;
    public float m_spawnDelayMaxDecrease = 1;
    public AnimationCurve m_initialVelocityAnimator;
    public Vector2 m_initialVelocityMaxIncrease;
    #endregion


    #region Unity Api
    private void OnEnable()
    {
        m_enemySpawner = GetComponent<EnemySpawnerController>();
        m_repeater = GetComponent<Repeater>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_initialVelocity = m_enemySpawner.m_initialVelocity;
        m_repeatTime = m_repeater.repeatTime;
    }

    // Update is called once per frame
    private void Update()
    {
        m_time += Time.deltaTime * m_timeScale;
        m_enemySpawner.m_initialVelocity = m_initialVelocity + m_initialVelocityMaxIncrease * m_initialVelocityAnimator.Evaluate(m_time);
        m_repeater.repeatTime = m_repeatTime - m_spawnDelayMaxDecrease * m_spawnDelayAnimator.Evaluate(m_time);
    }

    private void OnDisable()
    {
        m_enemySpawner.enabled = false;
    }
    #endregion


    #region Main Methods

    #endregion


    #region Utils

    #endregion


    #region Private and Protected
    private Vector2 m_initialVelocity;
    private float m_repeatTime;
    private EnemySpawnerController m_enemySpawner;
    private Repeater m_repeater;
    #endregion


}
