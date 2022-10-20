using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewDetector : MonoBehaviour
{
    [SerializeField] private Enemy m_Enemy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(3))
        {
            m_Enemy.ViewPlayer();
        }
    }
}
