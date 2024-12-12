using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Capsule_Controler : MonoBehaviour
{
    public Vector3 playerMovementInput;
    public Vector2 playerMouseInput;
    public float xRot = 0;
    public float yRot = 0;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] public Transform playerCamera;
    [SerializeField] private float speed;
    [SerializeField] public float sensitivity;
    [SerializeField] private float jumpForce;
    private float tempSpeed;
    
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
        playerMouseInput = new Vector2(xAngle, yAngle);
        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        MovePlayer();
        if (xAngle != 0 || yAngle != 0)
        {
            RotatePlayer();
        }
        
    }

    private void RotatePlayer()
    {
        yRot += playerMouseInput.x * sensitivity * Time.deltaTime;
        xRot -= playerMouseInput.y * sensitivity * Time.deltaTime;
        if (xRot < -70 || xRot > 70)
        {
            xRot += playerMouseInput.y * sensitivity * Time.deltaTime;
        }
        transform.localRotation = Quaternion.Euler(0f, yRot, 0f);        
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
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
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = tempSpeed * 1.5f;            
        }
        else
        {
            speed = tempSpeed;          
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

    