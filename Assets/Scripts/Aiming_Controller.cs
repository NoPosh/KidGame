using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_Controller : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    private float originalZoom;
    private Vector3 originalWeaponPosition;

    private void Start()
    {
        originalZoom = _Camera.fieldOfView;
        originalWeaponPosition = transform.localPosition;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _Camera.fieldOfView = 50f;
            transform.localPosition = new Vector3(0,-0.1f,0.7f);
        }
        else
        {
            _Camera.fieldOfView = originalZoom;
            transform.localPosition = originalWeaponPosition;
        }

    }
}
