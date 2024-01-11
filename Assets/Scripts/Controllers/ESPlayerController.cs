using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class ESPlayerController : MonoBehaviour
{
    private static ESPlayerController _instance;

    public float Gravity;
    
    public PlayerCameraSettings cameraSettings;

    private CharacterController _characterController;
    private ESPlayerInput _playerInput;

    private bool _isGrounded;
    private float _verticalSpeed;
    private float _forwardSpeed;
    private float _desiredForwardSpeed;

    private const float _stickingGravityProportion = 0.3f;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<ESPlayerInput>();
    }

    private void FixedUpdate()
    {
        CalculateForwardMovement();
        CalculateVerticalMovement();
    }

    void CalculateForwardMovement()
    {
        Vector2 moveInput = _playerInput.MoveInput;
        if (moveInput.sqrMagnitude > 1f)
            moveInput.Normalize();
    }

    void CalculateVerticalMovement()
    {
        if (_isGrounded)
        {
            
        }
        else
        {
            _verticalSpeed = -Gravity * _stickingGravityProportion;

            if (_playerInput.JumpInput)
            {
                Debug.Log("Jump");
            }
        }
    }


    public static ESPlayerController Instance => _instance;

}
