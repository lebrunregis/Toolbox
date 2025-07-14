using Pooling.Runtime;
using UnityEngine;

namespace Projectile.Runtime
{
    public class LaserShooter : MonoBehaviour
    {
        public GameObjectPool laserPool;
        public Transform target;
        public float laserSpeed = 10;
        public float laserRange = 5;
        public int laserDamage = 1;
        public float laserDamageMultiplier;
        public float timeToLive = 10;

        public void Shoot()
        {
            GameObject laser = laserPool.GetGameObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            laser.transform.rotation = transform.rotation;
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this.gameObject;
                laserController.homingTarget = null;
            }
        }


        public void ShootAt()
        {
            GameObject laser = laserPool.GetGameObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            RotateTowards(laser, new Vector2(target.position.x, target.position.y));
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this.gameObject;
                laserController.homingTarget = null;
            }
        }

        public void ShootAt(Transform target)
        {

            GameObject laser = laserPool.GetGameObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            RotateTowards(laser, new Vector2(target.position.x, target.position.y));
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this.gameObject;
                laserController.homingTarget = null;
            }
        }

        public void HomingShootAt()
        {
            GameObject laser = laserPool.GetGameObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            RotateTowards(laser, new Vector2(target.position.x, target.position.y));
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this.gameObject;
                laserController.homingTarget = target.gameObject;
            }
        }

        public void HomingShootAt(Transform target)
        {
            GameObject laser = laserPool.GetGameObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            RotateTowards(laser, new Vector2(target.position.x, target.position.y));
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this.gameObject;
                laserController.homingTarget = target.gameObject;
            }
        }

        private static void RotateTowards(GameObject go, Vector2 target)
        {
            Vector2 direction = (target - (Vector2)go.transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var offset = -90f;
            go.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }
    }
}
