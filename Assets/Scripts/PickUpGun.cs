using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    private InputManager inputManager;       
    private Camera playerCamera;
    [SerializeField]
    private float distance =20f;
    private GameObject currentWeapon;
    public static bool canPickUp;
    

    void Start()
    {
        playerCamera = Camera.main;        
        inputManager = InputManager.Instance;
        canPickUp = true;
    }   
    private void PickUp()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Weapon")
            {
                currentWeapon = hit.transform.parent.gameObject;
                currentWeapon.AddComponent<Rigidbody>().isKinematic = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = transform.localPosition;
                currentWeapon.transform.position = transform.position;
                currentWeapon.transform.localEulerAngles = new Vector3(-49.869f, 43.437f, 29.188f);
                currentWeapon.transform.localPosition = new Vector3(-0.1f, -0.059f, 0.164f);
                canPickUp = false;
                
            }
        }
    }

    private void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        canPickUp = true;
        currentWeapon = null;
    }
    void Update()
    {
        if (inputManager.GetPlayerPickUp() && canPickUp == true)
        {
            PickUp();
        }
        if (inputManager.GetPlayerDrop() && canPickUp == false)
        {
            Drop();
        }
    }
}
