using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private float v;
    private float h;
    private bool isJumping;
    [SerializeField] private Animator animator;
    [SerializeField] private Capsule_Controler capsule;
    [SerializeField] private RayCast gun;
    [SerializeField] private Transform rightHandEl;
    [SerializeField] private Transform leftHandEl;
    [SerializeField] private Transform spineUpper;
    private float yRotation;
    void Start()
    {
    
    }
    void Update()
    {

        HandsToGun();
        //HeadToCamera();
        //v = capsule.GetPlayerMovementInput().x;
        //h = capsule.GetPlayerMovementInput().z;
        //isJumping = capsule.GetPlayerJump();

        //animator.SetBool("isJumping", isJumping);
        //animator.SetFloat("v", v);
        //animator.SetFloat("h", h);
    }

   
    void HandsToGun()
    {
        rightHandEl.transform.position = gun.rightHandLoc.transform.position;
        //Vector3 pogr = new Vector3(0, -5, 0);
        //Quaternion q = new Quaternion(0, 0, 0, 0);
        //leftHandEl.SetLocalPositionAndRotation(pogr, q);
        leftHandEl.transform.position = gun.leftHandLoc.transform.position;
        
    }
    
    void HeadToCamera()
    {
        spineUpper.transform.localRotation = Quaternion.Euler(capsule.xRot, 0f, 0f);
        
    }
}
