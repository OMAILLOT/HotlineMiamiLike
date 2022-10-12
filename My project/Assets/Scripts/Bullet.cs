using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    // Update is called once per frame

    void Update()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));

        RaycastHit2D hit = new RaycastHit2D();
        hit.distance = 0.5f;
        //hit.rigidbody.velocity = Vector2.down * (speed * Time.deltaTime) ;
    }
}
