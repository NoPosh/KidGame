using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckRotation : MonoBehaviour
{
    private InputManager inputManager;
    private float xRot;
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    
    void Update()
    {
        xRot += inputManager.GetPlayerLook().y * 10f * Time.deltaTime * (-1f);
        xRot = Mathf.Clamp(xRot, -10, 30);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
}
