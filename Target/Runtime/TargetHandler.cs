using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private BoxCollider spawn;
    [SerializeField] private GameObject target;
    [SerializeField] private ScoreHandler scoreHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        target = Instantiate(targetPrefab);
        target.TryGetComponent<TargetBehaviour>(out TargetBehaviour targetBehaviour);
        targetBehaviour.targetHandler = this;
        targetBehaviour.scoreHandler = scoreHandler;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void RelocateTarget()
    {

        Vector3 pos = spawn.transform.position + spawn.center;
        Vector3 range = spawn.size;
        Vector3 randomizePosition = new Vector3(UnityEngine.Random.Range(-range.x, range.x),
                                                                           UnityEngine.Random.Range(-range.y, range.y),
                                                                           UnityEngine.Random.Range(-range.z, range.z)) / 2;
        target.transform.position = pos + randomizePosition;
    }
}
