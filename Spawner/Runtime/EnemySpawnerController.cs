using System.Collections.Generic;
using Tools;
using UnityEngine;

[RequireComponent(typeof(Repeater))]
public class EnemySpawnerController : MonoBehaviour
{
    public Transform m_player;
    public List<Transform> m_spawnLocations = new();
    public GameObjectPool m_enemyPool;
    private List<GameObject> m_activeEnemies = new();
    public int m_waveSize = 1;
    private Repeater m_repeater;
    public float m_pushForce = 0;
    public float m_spawnSpread = 0;
    public int m_furthestSpawnRandomRange = 1;
    public Vector2 m_initialVelocity = Vector2.zero;
    public EnemySpawnerEnum.EnemySpawnBehavior m_spawnBehavior = EnemySpawnerEnum.EnemySpawnBehavior.Random;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_repeater = GetComponent<Repeater>();
    }

    public void OnEnable()
    {
        if (m_spawnLocations.Count == 0)
        {
            Debug.Log("No spawn set! Disabling self.");
        }
    }

    private void OnDisable()
    {
        m_repeater.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < m_activeEnemies.Count; i++)
        {
            if (!m_activeEnemies[i].activeInHierarchy)
            {
                m_activeEnemies.RemoveAt(i);
            }
        }
        if (m_activeEnemies.Count == 0 && !m_repeater.isActiveAndEnabled)
        {
            Debug.Log("Starting new wave");
            StartNewWave();
        }
    }

    private void StartNewWave()
    {
        m_repeater.repeatCount = m_waveSize;
        m_repeater.enabled = true;
    }

    public void SpawnEnemy()
    {
        GameObject enemy = m_enemyPool.GetFirstAvailableObject();
        if (enemy != null)
        {
            m_activeEnemies.Add(enemy);
            switch (m_spawnBehavior)
            {
                case EnemySpawnerEnum.EnemySpawnBehavior.Furthest:
                    SpawnEnemyAtFurthestSpawn(enemy);
                    break;
                case EnemySpawnerEnum.EnemySpawnBehavior.Random:
                    SpawnEnemyAtRandomSpawn(enemy);
                    break;
            }

            enemy.SetActive(true);
            ApplyRigibodySettings(enemy);
        }
        else
        {
            Debug.Log("Over Pool limit!");
        }
    }

    private void ApplyRigibodySettings(GameObject enemy)
    {
        Rigidbody2D rb;
        if (enemy.TryGetComponent<Rigidbody2D>(out rb))
        {

            Vector2 dir = new Vector2() - new Vector2(enemy.transform.position.x, enemy.transform.position.y);
            rb.linearVelocity = m_initialVelocity;
            rb.AddForce(m_pushForce * dir.normalized);
        }
    }

    private void SpawnEnemyAtFurthestSpawn(GameObject enemy)
    {
        int furthestSpawnPoint = 0;
        float maxDistance = 0;
        int i = 0;

        do
        {
            float distance = Vector3.Distance(m_spawnLocations[i].transform.position, m_player.transform.position);
            if (maxDistance < distance)
            {
                maxDistance = distance;
                furthestSpawnPoint = i;
            }
            i++;
        } while (i < m_spawnLocations.Count);

        if (m_furthestSpawnRandomRange > 0)
        {
            furthestSpawnPoint = (furthestSpawnPoint + UnityEngine.Random.Range(-m_furthestSpawnRandomRange, m_furthestSpawnRandomRange)) % m_spawnLocations.Count;
        }

        enemy.transform.position = m_spawnLocations[furthestSpawnPoint].position + new Vector3(UnityEngine.Random.Range(-m_spawnSpread, m_spawnSpread), UnityEngine.Random.Range(-m_spawnSpread, m_spawnSpread), 0);
    }

    private void SpawnEnemyAtRandomSpawn(GameObject enemy)
    {
        int spawnId = Random.Range(0, m_spawnLocations.Count);
        enemy.transform.position = m_spawnLocations[spawnId].position + new Vector3(UnityEngine.Random.Range(-m_spawnSpread, m_spawnSpread), UnityEngine.Random.Range(-m_spawnSpread, m_spawnSpread), 0);
    }
}
