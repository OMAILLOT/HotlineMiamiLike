using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSoldier : Enemy
{
    [SerializeField] private float timeBeforeShoot;
    [SerializeField] private Transform pistolPlaceHolder;
    [SerializeField] private float randomPistolAngle;
    [Space(5)]
    [SerializeField] private float shootTimeInterval;
    [SerializeField] private float timeToRunOnPlayer;
    [SerializeField] private float timeBeforeContinueAnimation;

    private bool canShoot = true;
    

    private Vector3 currentPositionOnAnimation;

    protected override void Attack()
    {
        base.Attack();
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
            if (!isRunToPlayer)
            {
                walkAnimation.enabled = false;

                StartCoroutine(EnemyRunOnPlayer());
                StartCoroutine(WaitBeforeStopMove());
            }
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

    IEnumerator WaitBeforeStopMove()
    {
        yield return new WaitForSeconds(timeToRunOnPlayer);
        isRunToPlayer = false;
    }

    IEnumerator EnemyRunOnPlayer()
    {
        isRunToPlayer = true;
        currentPositionOnAnimation = transform.position;
        while (isRunToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, enemySpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(timeBeforeContinueAnimation);
        StartCoroutine(ReturnToPosition());

    }

    IEnumerator ReturnToPosition()
    {
        while (transform.position != currentPositionOnAnimation)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPositionOnAnimation, enemySpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        walkAnimation.enabled = true;
    }

    public override void Die()
    {
        StopAllCoroutines();
        base.Die();
    }

}
