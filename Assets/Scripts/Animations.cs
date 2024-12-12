using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animator;
    private InputManager inputManager;   

    private float x;
    private float y;
    private bool jump;
    private float sit;
    private bool pickUp;
    private bool drop;
    private float withGun;
    void Start()
    {            
        animator = GetComponent<Animator>();
        inputManager = InputManager.Instance;
    }
    void Update()
    {
        if (!PickUpGun.canPickUp) withGun = 1;
        else withGun = 0;

        pickUp = inputManager.GetPlayerPickUp();
        drop = inputManager.GetPlayerDrop();
        jump = inputManager.GetPlayerJump();
        sit = inputManager.GetPlayerSit();
        x = inputManager.GetPlayerMovement().x;
        y = inputManager.GetPlayerMovement().y;
        sendParameters();
    }   

    void sendParameters()
    {
        animator.SetFloat("x", x);
        animator.SetFloat("y", y);
        animator.SetFloat("sit", sit);
        if (jump) animator.SetTrigger("jump");
        if (pickUp) animator.SetTrigger("pickUp");
        if (drop) animator.SetTrigger("drop");
        animator.SetFloat("withGun", withGun);
    }
}
