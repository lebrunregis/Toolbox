using System.Collections.Generic;
using Tools;
using UnityEngine;

[RequireComponent(typeof(Repeater))]

public class SpawnerController<T> : MonoBehaviour where T : IPool
{
    public Transform player;
    public List<Transform> spawnLocations = new();
    public T objectPool;
    private readonly List<GameObject> activeObjects = new();
    public int waveSize = 1;
    private Repeater repeater;
    public float pushForce = 0;
    public float spawnSpread = 0;
    public int furthestSpawnRange = 0;
    public Vector2 initialVelocity = Vector2.zero;
    public SpawnerEnum.SpawnBehavior spawnBehavior = SpawnerEnum.SpawnBehavior.Random;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        repeater = GetComponent<Repeater>();
        repeater.enabled = false;

    }

    public void OnEnable()
    {
        if (spawnLocations.Count == 0)
        {
            Debug.Log("No spawn set! Disabling self.");
        }

        if (objectPool == null)
        {
            Debug.Log("No object pool set! Disabling self.");
        }
    }

    private void OnDisable()
    {
        repeater.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < activeObjects.Count; i++)
        {
            if (!activeObjects[i].activeSelf)
            {
                activeObjects.RemoveAt(i);
            }
        }
        if (activeObjects.Count == 0 && !repeater.isActiveAndEnabled)
        {
            Debug.Log("Starting new wave");
            StartNewWave();
        }
    }

    private void StartNewWave()
    {
        repeater.repeatCount = waveSize;
        repeater.enabled = true;
    }

    public void SpawnObject()
    {
        GameObject go = objectPool.GetGameObject();
        if (go != null)
        {
            activeObjects.Add(go);
            switch (spawnBehavior)
            {
                case SpawnerEnum.SpawnBehavior.Furthest:
                    SpawnObjectAtFurthestSpawn(go);
                    break;
                case SpawnerEnum.SpawnBehavior.Random:
                    SpawnObjectAtRandomSpawn(go);
                    break;
            }

            go.SetActive(true);
            ApplyRigibodySettings(go);
        }
        else
        {
            Debug.Log("Over Pool limit!");
        }
    }

    private void ApplyRigibodySettings(GameObject enemy)
    {
        if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {

            Vector2 dir = new Vector2() - new Vector2(enemy.transform.position.x, enemy.transform.position.y);
            rb.linearVelocity = initialVelocity;
            rb.AddForce(pushForce * dir.normalized);
        }
    }

    private void SpawnObjectAtFurthestSpawn(GameObject go)
    {
        int furthestSpawnPoint = FindFurthestSpawn();
        SpawnObjectAtSpawn(go, furthestSpawnPoint);
    }

    private int FindFurthestSpawn()
    {
        int furthestSpawnPoint = 0;
        float maxDistance = 0;
        int i = 0;

        do
        {
            float distance = Vector3.Distance(spawnLocations[i].transform.position, player.transform.position);
            if (maxDistance < distance)
            {
                maxDistance = distance;
                furthestSpawnPoint = i;
            }
            i++;
        } while (i < spawnLocations.Count);

        if (furthestSpawnRange > 0)
        {
            furthestSpawnPoint = (furthestSpawnPoint + Random.Range(-furthestSpawnRange, furthestSpawnRange)) % spawnLocations.Count;
        }
        return furthestSpawnPoint;
    }

    private void SpawnObjectAtRandomSpawn(GameObject go)
    {
        int spawnId = Random.Range(0, spawnLocations.Count);
        SpawnObjectAtSpawn(go, spawnId);
    }

    private void SpawnObjectAtSpawn(GameObject go, int spawnId)
    {
        go.transform.position = spawnLocations[spawnId].position + new Vector3(Random.Range(-spawnSpread, spawnSpread), Random.Range(-spawnSpread, spawnSpread), 0);
        go.SetActive(true);
    }
}
