using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject character;
    Rigidbody rb;
    public GameObject mainCamera;
    public bool isJumping;
    public float speed = 3000.0f;
    public float jumpingForce = 500.0f;
    
    public float verticalMouseSensetivity = 100.0f;
    public float horizontalMouseSensetivity = 100.0f;
    float xRotation = 0.0f;
    float fallingCoef = 5.0f;

    private void Start()
    {
        rb = character.GetComponentInChildren<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;       //Фиксируем курсор и скрываем
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        CharacterMovement();
        CharacterRotation();
        CharacterJumping();
    }
    void CharacterJumping()
    {
        if (Input.GetAxis("Jump")!=0 && !isJumping)
        {
            isJumping = true;
            Vector3 jumpingDirection = character.transform.up;
            rb.AddForce(jumpingDirection * jumpingForce, ForceMode.Impulse);
        }
        if (rb.velocity.y != 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallingCoef * Time.deltaTime;
        }
    }
    void CharacterMovement()            //Движение персонажа
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput > 0)           //Проверка горизонтального ввода
        {
            Vector3 horVector = character.transform.right.normalized * speed;      //Вектор вправо
            rb.AddForce(horVector, ForceMode.Force);      //Добавляем импульс телу     
        }
        else if (horizontalInput < 0)
        {
            Vector3 horVector = -character.transform.right.normalized * speed;      //Вектор влево
            rb.AddForce(horVector, ForceMode.Force);      //Добавляем импульс телу
        }

        if (verticalInput > 0)         //Проверка вертикального импута
        {
            Vector3 vertVector = character.transform.forward.normalized * speed;       //Вектор прямо
            rb.AddForce(vertVector, ForceMode.Force);         //Импульс
        }
        else if (verticalInput < 0)
        {
            
            Vector3 vertVector = -character.transform.forward.normalized * speed;       //Вектор прямо
            rb.AddForce(vertVector, ForceMode.Force);         //Импульс
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            Debug.Log("Ground");
        }
      
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
    void CharacterRotation()        //Поворот камеры + персонажа
    {
        
        float mouseX = Input.GetAxis("Mouse X") * horizontalMouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalMouseSensetivity * Time.deltaTime;

        if (mouseX != 0)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            rb.constraints |= RigidbodyConstraints.FreezeRotationX;
            rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationY; 
            rb.constraints |= RigidbodyConstraints.FreezeRotationX;
            rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        character.transform.Rotate(Vector3.up * mouseX);
    }

}
