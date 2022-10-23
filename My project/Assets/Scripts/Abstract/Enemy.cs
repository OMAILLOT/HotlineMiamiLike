using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isDie;

    [SerializeField] private Collider2D viewRange;
    [SerializeField] private Collider2D hitBoxEnemy;
    [SerializeField] protected GameObject newRenderer;
    [SerializeField] protected float enemySpeed;
    [SerializeField] protected Animator walkAnimation;
    [SerializeField] protected GameObject itemWhenDie;

    [SerializeField] private ParticleSystem bloodOnFloorParticle;

    protected bool isRunToPlayer = false;
    public virtual void Die()
    {
        walkAnimation.enabled = false;
        hitBoxEnemy.enabled = false;
        Vector3 currentPosition = transform.position;
        isDie = true;
        isRunToPlayer = false;
        GameManager.Instance.TotalEnemyReduce();
        StartCoroutine(TurnAroundBeforeDie());
        transform.position = currentPosition;
        Instantiate(bloodOnFloorParticle, transform.position, transform.rotation);
        Instantiate(itemWhenDie, transform.position, transform.rotation);
    }

    public virtual void ViewPlayer()
    {
        Vector2 targetDir = PlayerController.Instance.transform.position - newRenderer.transform.position;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f;
        newRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Attack();
    }

    protected virtual void Attack()
    {

    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    IEnumerator TurnAroundBeforeDie()
    {
        yield return new WaitForSeconds(1f);
        DisableEnemy();
    }

   

}
