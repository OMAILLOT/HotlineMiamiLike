using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] private float m_Speed;
    [SerializeField] private GameObject renderer;
    [SerializeField] private Transform pistolPlaceHolder;
    [SerializeField] private float randomPistolAngle;
    [SerializeField] private float reloadTime;

    [SerializeField] private int magasinMaxBullet;

    private int currentMagasinNumber;
    private InputMovement m_Movement;
    private bool isReloaded;
   



    public void Init()
    {
        m_Movement = new InputMovement();
        m_Movement.Enable();

        currentMagasinNumber = magasinMaxBullet;

    }
    private void Update()
    {
        var moveDirection = m_Movement.Player.Moove.ReadValue<Vector2>() ;
        transform.Translate(moveDirection * (m_Speed * Time.deltaTime));

        Vector2 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f;
        renderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            currentMagasinNumber--;
            if (currentMagasinNumber < 0)
            {
                if (!isReloaded)
                {
                    StartCoroutine(ReloadTime());
                }
                return;
            }

            PoolManager.Instance.SpawnFromPool("Bullet",
                                                    pistolPlaceHolder.position,
                                                    new Quaternion(renderer.transform.rotation.x,
                                                    renderer.transform.rotation.y,
                                                    Random.Range(renderer.transform.rotation.z - randomPistolAngle, renderer.transform.rotation.z + randomPistolAngle),
                                                    renderer.transform.rotation.w));
        }
    }


    IEnumerator ReloadTime()
    {
        isReloaded = true;
        yield return new WaitForSeconds(reloadTime);
        currentMagasinNumber = magasinMaxBullet;
        isReloaded = false;
    }

    public void PlayerDie()
    {
        renderer.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 




}
