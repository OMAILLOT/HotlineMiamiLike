using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask layerTouch;
    [SerializeField] private ParticleSystem hitEnemyBlood;
    [SerializeField] private GameObject renderer;

    


    bool isHited;

    public void OnObjectSpawn()
    {
        //throw new System.NotImplementedException();
        renderer.SetActive(true);
    }

    void Update()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime)); //direction de la bullet

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, layerTouch);
    
        if (hit && !isHited)
        {
            isHited = true;

            print("Layertouch value : " + hit.transform.gameObject.layer);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                hit.collider.GetComponent<PlayerController>().PlayerDie();
            } else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().Die();
            }
            StartCoroutine(waitBeforeDisable(hit));
        }
        
    }

    IEnumerator waitBeforeDisable(RaycastHit2D hit)
    {
        renderer.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        isHited = false;
        gameObject.SetActive(false);
    }
}
