using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ESPlayerCameraSettings : MonoBehaviour
{
    [Serializable]
    public struct InvertSettings
    {
        public bool invertX;
        public bool invertY;
    }
    
    public Vector3 Distance;

    public Transform FollowTarget;
    public Transform LookTarget;
    
    public CinemachineFreeLook keyboardAndMouseCamera;
    public CinemachineFreeLook controllerCamera;
    
    public InvertSettings keyboardAndMouseInvertSettings;
    public InvertSettings controllerInvertSettings;

    private void Reset()
    {
        Transform keyboardAndMouseCameraTransform = transform.Find("KeyboardAndMouseFreeLookRig");
        if (keyboardAndMouseCameraTransform != null)
            keyboardAndMouseCamera = keyboardAndMouseCameraTransform.GetComponent<CinemachineFreeLook>();

        Transform controllerCameraTransform = transform.Find("ControllerFreeLookRig");
        if (controllerCameraTransform != null)
            controllerCamera = controllerCameraTransform.GetComponent<CinemachineFreeLook>();

        ESPlayerController playerController = FindObjectOfType<ESPlayerController>();
        if (playerController != null && playerController.name == "Ellen")
        {
            FollowTarget = playerController.transform;
            LookTarget = FollowTarget.Find("HeadTarget");

            if (playerController.cameraSettings == null)
                playerController.cameraSettings = this;
        }
    }

    private void Update()
    {
        keyboardAndMouseCamera.Follow = FollowTarget;
        keyboardAndMouseCamera.LookAt = LookTarget;
        keyboardAndMouseCamera.m_XAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertX;
        keyboardAndMouseCamera.m_YAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertY;
        
        // controllerCamera.m_XAxis.m_InvertInput = controllerInvertSettings.invertX;
        // controllerCamera.m_YAxis.m_InvertInput = controllerInvertSettings.invertY;
        // controllerCamera.Follow = FollowTarget;
        // controllerCamera.LookAt = LookTarget;

        // keyboardAndMouseCamera.Priority = inputChoice == InputChoice.KeyboardAndMouse ? 1 : 0;
        // controllerCamera.Priority = inputChoice == InputChoice.Controller ? 1 : 0;
    }
}
