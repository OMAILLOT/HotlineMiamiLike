using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask layerTouch;
    [SerializeField] private ParticleSystem hitEnemyBlood;
    [SerializeField] private GameObject renderer;


    bool isHited;
    void Update()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime)); //direction de la bullet

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, layerTouch);

        if (hit && !isHited)
        {
            isHited = true;
            Debug.Log(hit.collider.name);
            StartCoroutine(waitBeforeDisable(hit));
            if (Physics2D.Raycast(transform.position, Vector2.up, 0.01f, 10))
            {
                GetComponent<Enemy>().Die();
            }
        }
        
    }

    IEnumerator waitBeforeDisable(RaycastHit2D hit)
    {
        hitEnemyBlood.Play();
        renderer.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        hit.collider.gameObject.SetActive(false);
        isHited = false;
        gameObject.SetActive(false);
    }
}
