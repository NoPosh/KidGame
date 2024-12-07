using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private float v;
    private float h;
    private bool isJumping;
    Animator animator;
    Capsule_Controler capsule;
    void Start()
    {
        animator = GetComponent<Animator>();
        capsule = GetComponent<Capsule_Controler>();
    }
    void Update()
    {
        v = capsule.GetPlayerMovementInput().x;
        h = capsule.GetPlayerMovementInput().z;
        isJumping = capsule.GetPlayerJump();

        animator.SetBool("isJumping", isJumping);
        animator.SetFloat("v", v);
        animator.SetFloat("h", h);
    }
}
