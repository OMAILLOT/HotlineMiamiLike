using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private GameObject renderer;
    private InputMovement m_Movement;



    private void Start()
    {
        m_Movement = new InputMovement();
        m_Movement.Enable();

        //This outputs what language your system is in
        Debug.Log("This system is in " + Application.systemLanguage);
    }
    private void Update()
    {
        var moveDirection = m_Movement.Player.Moove.ReadValue<Vector2>() ;
        transform.Translate(moveDirection * (m_Speed * Time.deltaTime));

        Vector2 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg);
        renderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


}
