using Tools;
using UnityEngine;

[RequireComponent(typeof(CircleRenderer))]
[RequireComponent(typeof(GameObjectPool))]
public class SpawnerController : MonoBehaviour
{
    public Vector2 particleVelocity = new Vector2(5, 0);
    private Tools.GameObjectPool particlePool;
    public float spawnTime = 0.1f;
    private float spawnRange;
    public float spawnDelta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        spawnRange = GetComponent<CircleRenderer>().radius;
        particlePool = GetComponent<GameObjectPool>();
    }

    // Update is called once per frame
    private void Update()
    {
        spawnDelta -= Time.deltaTime;
        if (spawnDelta < 0)
        {
            spawnDelta = spawnTime;
            GameObject particle = particlePool.GetFirstAvailableObject();
            if (particle != null)
            {
                int angle = Random.Range(0, 359);
                float dist = Random.Range(0, spawnRange);
                particle.transform.position = transform.position + new Vector3(Mathf.Sin(angle) * dist, Mathf.Cos(angle) * dist, 0);
                particle.SetActive(true);
                Rigidbody rb = particle.GetComponent<Rigidbody>();
                rb.linearVelocity = particleVelocity;
            }
        }
    }
}
