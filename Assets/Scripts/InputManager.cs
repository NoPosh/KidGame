using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;   
    private static InputManager instance;
    public static InputManager Instance {
        get {
            return instance;
        }
    }
    void Awake()
    {     
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        playerControls = new PlayerControls();
        playerControls.Enable();
    }
    
    void Update()
    {
        
    }
    public Vector2 GetPlayerMovement() { return playerControls.Player.Movement.ReadValue<Vector2>(); }
    public Vector2 GetPlayerLook() { return playerControls.Player.Look.ReadValue<Vector2>(); }
    public bool GetPlayerJump() { return playerControls.Player.Jump.triggered; }
    public float GetPlayerSit() { return playerControls.Player.Sit.ReadValue<float>(); }
    public bool GetPlayerPickUp() { return playerControls.Player.PickUp.triggered; }
    public bool GetPlayerDrop() { return playerControls.Player.Drop.triggered; }
    public float GetPlayerRun() { return playerControls.Player.Run.ReadValue<float>(); }
}
