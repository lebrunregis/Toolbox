using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField] public ScoreHandler scoreHandler;
    [SerializeField] public TargetHandler targetHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void Hit(RaycastHit hit)
    {
        scoreHandler.ScoreHit((int)(Vector3.Distance(hit.point, this.transform.position) * hit.distance));
        targetHandler.RelocateTarget();
    }
}
