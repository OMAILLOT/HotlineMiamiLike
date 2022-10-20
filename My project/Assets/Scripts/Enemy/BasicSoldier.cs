using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSoldier : Enemy
{
    [SerializeField] private float timeBeforeShoot;
    [SerializeField] private Transform pistolPlaceHolder;
    [SerializeField] private float randomPistolAngle;
    [SerializeField] private float shootTimeInterval;

    private bool canShoot = true;
    protected override void Attack()
    {
        base.Attack();
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        PoolManager.Instance.SpawnFromPool("EnemyBullet",
                                        pistolPlaceHolder.position,
                                        new Quaternion(newRenderer.transform.rotation.x,
                                        newRenderer.transform.rotation.y,
                                        Random.Range(newRenderer.transform.rotation.z - randomPistolAngle, newRenderer.transform.rotation.z + randomPistolAngle),
                                        newRenderer.transform.rotation.w));
        yield return new WaitForSeconds(shootTimeInterval);
        canShoot = true;
        
    }
}
