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
        Cursor.lockState = CursorLockMode.Locked;       //��������� ������ � ��������
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
    void CharacterMovement()            //�������� ���������
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput > 0)           //�������� ��������������� �����
        {
            Vector3 horVector = character.transform.right.normalized * speed;      //������ ������
            rb.AddForce(horVector, ForceMode.Force);      //��������� ������� ����     
        }
        else if (horizontalInput < 0)
        {
            Vector3 horVector = -character.transform.right.normalized * speed;      //������ �����
            rb.AddForce(horVector, ForceMode.Force);      //��������� ������� ����
        }

        if (verticalInput > 0)         //�������� ������������� ������
        {
            Vector3 vertVector = character.transform.forward.normalized * speed;       //������ �����
            rb.AddForce(vertVector, ForceMode.Force);         //�������
        }
        else if (verticalInput < 0)
        {
            
            Vector3 vertVector = -character.transform.forward.normalized * speed;       //������ �����
            rb.AddForce(vertVector, ForceMode.Force);         //�������
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
    void CharacterRotation()        //������� ������ + ���������
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
