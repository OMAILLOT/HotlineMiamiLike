using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Collider2D viewRange;
    [SerializeField] protected GameObject newRenderer;
    public virtual void Die()
    {
        gameObject.SetActive(false);
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

}
