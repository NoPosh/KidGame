using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Capsule_Controler : MonoBehaviour
{
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot = 0;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       
        Cursor.visible = false;
    }
    private void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput).normalized * speed;
        playerBody.MovePosition(playerBody.position + MoveVector * Time.deltaTime);
        if (StayOnGrond())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        
    }

    private void MovePlayerCamera()
    {
        xRot -= playerMouseInput.y * sensitivity * Time.deltaTime;
        if(xRot < -90 || xRot > 90)
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

    
