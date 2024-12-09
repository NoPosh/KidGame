using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Capsule_Controler : MonoBehaviour
{
    private static Vector3 playerMovementInput;
    public Vector2 playerMouseInput;
    public float xRot = 0;

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] public Transform playerCamera;
    [SerializeField] private float speed;
    [SerializeField] public float sensitivity;
    [SerializeField] private float jumpForce;
    private float tempSpeed;
    private bool isJumping;
    private bool isRunning;
    private Vector3 originalCameraPosition;
    


    private void Start()
    {
        tempSpeed = speed;
        originalCameraPosition = playerCamera.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    private void Update()
    {
        float xAngle = Input.GetAxis("Mouse X");
        float yAngle = Input.GetAxis("Mouse Y");
        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        playerMouseInput = new Vector2(xAngle, yAngle);
        MovePlayer();
        if (xAngle != 0 || yAngle != 0)
        {
            MovePlayerCamera();
        }
    }

    public Vector3 GetPlayerMovementInput()
    {
        return playerMovementInput;
    }
    public bool GetPlayerJump()
    {
        return isJumping;
    }
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput).normalized * speed;
        playerBody.MovePosition(playerBody.position + MoveVector * Time.deltaTime);
        if (StayOnGrond())
        {
            isJumping = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
               
            }

        }
        else
        {
            isJumping = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = tempSpeed * 1.5f;
            isRunning = true;
            
        }
        else
        {
            speed = tempSpeed;
            isRunning = false;
           
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerCamera.localPosition = originalCameraPosition - Vector3.up;
            
        }
        else
        {
            playerCamera.localPosition = originalCameraPosition;
        }
    }

    private void MovePlayerCamera()
    {
        xRot -= playerMouseInput.y * sensitivity * Time.deltaTime;
        if (xRot < -90 || xRot > 90)
        {
            xRot += playerMouseInput.y * sensitivity * Time.deltaTime;
        }
        transform.Rotate(0f, playerMouseInput.x * sensitivity * Time.deltaTime, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    private bool StayOnGrond()
    {
        bool isStaingOnGround = true;
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.distance > 1.01f)
        {
            isStaingOnGround = false;
        }
        return isStaingOnGround;
    }
}

    
