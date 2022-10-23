using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewDetector : MonoBehaviour
{
    [SerializeField] private Enemy m_Enemy;
    [SerializeField] private LayerMask layerTouch;
    private bool isDetectPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isDetectPlayer = true;
        }
    }

    private void Update()
    {
        if (isDetectPlayer && !m_Enemy.isDie)
        {
            RaycastHit2D hit = Physics2D.Raycast(m_Enemy.transform.position, 
                                                PlayerController.Instance.transform.position - m_Enemy.transform.position, 10f,
                                                layerTouch);

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                m_Enemy.ViewPlayer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isDetectPlayer = false;
        }
    }
}
