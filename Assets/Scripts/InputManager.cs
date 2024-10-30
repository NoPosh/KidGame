using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject character;
    Rigidbody rb;
    public GameObject mainCamera;

    public float speed = 100.0f;

    
    public float verticalMouseSensetivity = 100.0f;
    public float horizontalMouseSensetivity = 100.0f;
    float xRotation = 0.0f;

    //Нужен поворот камеры вверх-вниз + поворот персонажа влево-вправо
    

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
    }

    void CharacterMovement()            //Движение персонажа
    {
        

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        

        //Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized * speed;
        //rb.MovePosition(movement + rb.position);
        //character.transform.forward
        //rb.velocity = new Vector3(movement.x, movement.y, movement.z);


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

    void CharacterRotation()        //Поворот камеры + персонажа
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalMouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalMouseSensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        character.transform.Rotate(Vector3.up * mouseX);
        
        

    }

}
