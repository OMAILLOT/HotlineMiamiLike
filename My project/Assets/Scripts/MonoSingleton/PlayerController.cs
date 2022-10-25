using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoSingleton<PlayerController>
{
    [HideInInspector] public bool isInMenu = true;

    [SerializeField] private float m_Speed;
    [SerializeField] private GameObject renderer;
    [SerializeField] private Transform pistolPlaceHolder;
    [SerializeField] private float randomPistolAngle;
    [SerializeField] private float reloadTime;
    [SerializeField] private int numberOfMagasin;

    [SerializeField] private int magasinMaxBullet;

    private int currentMagasinNumber;
    private InputMovement m_Movement;
    private bool isReloaded;



    #region public
    public void Init()
    {
        isInMenu = true;
        m_Movement = new InputMovement();
        m_Movement.Enable();

        currentMagasinNumber = magasinMaxBullet;

    }
    public void PlayerDie()
    {
        renderer.SetActive(false);
        UiManager.Instance.OpenLoosePanel();
    }

    public void IncreaseMagasinNumber()
    {
        numberOfMagasin++;
        UiManager.Instance.UpdateMagasinNumber(numberOfMagasin);
    }
    #endregion

    #region private
    private void Update()
    {
        if (isInMenu) return;

        var moveDirection = m_Movement.Player.Moove.ReadValue<Vector2>() ;
        transform.Translate(moveDirection * (m_Speed * Time.deltaTime));

        Vector2 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f;
        renderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            if (!isReloaded)
            {
                PoolManager.Instance.SpawnFromPool("Bullet",
                                                    pistolPlaceHolder.position,
                                                    new Quaternion(renderer.transform.rotation.x,
                                                    renderer.transform.rotation.y,
                                                    Random.Range(renderer.transform.rotation.z - randomPistolAngle, renderer.transform.rotation.z + randomPistolAngle),
                                                    renderer.transform.rotation.w));
                currentMagasinNumber--;

                UiManager.Instance.DecreaseBulletOnUi(currentMagasinNumber);

                if (currentMagasinNumber <= 0)
                {
                    StartCoroutine(CheckNumberOfMagasinLeft());
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadTime());
        }
    }


    IEnumerator CheckNumberOfMagasinLeft()
    {
        while (numberOfMagasin <= 0)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(ReloadTime());
    }

    IEnumerator ReloadTime()
    {
        if (numberOfMagasin > 0)
        {
            numberOfMagasin--;
            isReloaded = true;
            yield return new WaitForSeconds(reloadTime);
            currentMagasinNumber = magasinMaxBullet;
            UiManager.Instance.ReloadBulletUi(numberOfMagasin);
            isReloaded = false;
        }
    }

    #endregion


}
