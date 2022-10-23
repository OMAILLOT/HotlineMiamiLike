using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollectible : Collectible
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.IsPlayer(collision))
        {
            PlayerController.Instance.IncreaseMagasinNumber();
            gameObject.SetActive(false);
        }
    }
}
