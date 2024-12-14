using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CinemachinePOV : CinemachineExtension
{
    [SerializeField]
    private float borderAngle = 70f;
    [SerializeField]
    public float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;

    private InputManager inputManager;
    private Vector3 startingRot;
    private CinemachineCameraOffset offset;

    protected override void Awake()
    {
        offset = GetComponent<CinemachineCameraOffset>();
        inputManager = InputManager.Instance;
        base.Awake();
    }

    private void Start()
    {
        
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if (startingRot == null) startingRot = transform.localRotation.eulerAngles;
                Vector2 deltaInput = inputManager.GetPlayerLook();
                startingRot.x += deltaInput.x * Time.deltaTime * horizontalSpeed;
                startingRot.y += -1*deltaInput.y * Time.deltaTime * verticalSpeed;
                startingRot.y = Mathf.Clamp(startingRot.y, -borderAngle, borderAngle);
                state.RawOrientation = Quaternion.Euler(startingRot.y, startingRot.x, 0f);

                if (inputManager.GetPlayerSit() > 0) offset.enabled = true;
                else offset.enabled = false;
            }
        }
    }
}
