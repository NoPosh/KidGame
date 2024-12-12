using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private float yRot = 0;
    private Transform cameraTransform;
    [SerializeField]
    private Transform cameraObj;
    private Vector3 originalCameraPosition;
    private int withGun;


    [SerializeField]
    private float playerSpeed = 7f;
    [SerializeField]
    private float jumpHeight = 3f;
    [SerializeField]
    private float gravityValue = -9.81f;
    

    void Start()
    {
        cameraObj = GetComponent<Transform>();
        cameraTransform = Camera.main.transform;
        originalCameraPosition = cameraObj.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;        
        
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

    void Update()
    {
        
        groundedPlayer = StayOnGrond();
        if (groundedPlayer && playerVelocity.y < 0)
        {            
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();        
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        Vector3 camdir = cameraTransform.forward; camdir.y = 0;
        camdir.Normalize();
        move = camdir * move.z + cameraTransform.right.normalized * move.x;
        move.y = 0f;

        Vector2 deltaInput = inputManager.GetPlayerLook();
        yRot += deltaInput.x * Time.deltaTime * 10f;
        transform.localRotation = Quaternion.Euler(0f, yRot, 0f);

        if (inputManager.GetPlayerSit() == 1 && groundedPlayer)
        {
            //cameraTransform.localPosition = originalCameraPosition - Vector3.up;
            cameraObj.localPosition = originalCameraPosition - Vector3.up;
            controller.Move(move * Time.deltaTime * playerSpeed * 0.5f);
        }
        else controller.Move(move * Time.deltaTime * playerSpeed);

        if (inputManager.GetPlayerJump() && groundedPlayer && inputManager.GetPlayerSit() != 1)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}