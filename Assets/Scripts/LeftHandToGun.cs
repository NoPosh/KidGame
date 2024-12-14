using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandToGun : MonoBehaviour
{
    public Transform leftHand;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if (PickUpGun.canPickUp == false)
        {
            leftHand.position = transform.position;
            leftHand.localRotation = Quaternion.Euler(258.581f, 234.482f, -66.52499f);
        }
        
    }
}
