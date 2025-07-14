using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float laserSpeed = 10;
    public float laserRange = 5;
    public int laserDamage = 1;
    public float timeToLive = 10;
    public GameObject shooter;
    public GameObject homingTarget;
    public bool canBeReflected = true;

    private void Update()
    {
        if (homingTarget != null)
        {
            transform.LookAt(homingTarget.transform, transform.up);
        }
        transform.position += (transform.up * (laserSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //     AsteroidController asteroidController;
        //     EnemyController enemyController;
        //     PlayerController playerController;

        //     if (other.gameObject.TryGetComponent<AsteroidController>(out asteroidController))
        //    {
        //        asteroidController.TakeDamage(laserDamage);
        //       gameObject.SetActive(false);
        //   }
        //  else if (other.gameObject.TryGetComponent<EnemyController>(out enemyController))
        // {
        //    enemyController.TakeDamage(laserDamage);
        //   gameObject.SetActive(false);
        // }
        // else if (other.gameObject.TryGetComponent<PlayerController>(out playerController))
        // {
        //    gameObject.SetActive(false);
        // }
    }
}
