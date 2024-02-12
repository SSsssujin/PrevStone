using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ESPlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class ESPlayerMoveController : MonoBehaviour
{
    private static ESPlayerMoveController _instance;

    public float Gravity = 20;
    public float JumpSpeed = 10f;
    public float MaxForwardSpeed = 8f;
    public float MinTurnSpeed = 400f;      
    public float MaxTurnSpeed = 1200f;   

    private CharacterController _characterController;

    public ESPlayerCameraSettings cameraSettings;
    private ESPlayerInput _playerInput;

    private bool _isGrounded;
    private bool _isJumpable;
    private float _verticalSpeed;
    private float _forwardSpeed;
    private float _desiredForwardSpeed;
    private float _angleDiff;
    
    private Quaternion _targetRotation;
    public Vector3 TargetRotation => _targetRotation.eulerAngles;

    private const float c_stickingGravityProportion = 0.3f;
    private const float c_groundedRayDistance = 1f;
    private const float c_groundAcceleration = 20f;
    private const float c_groundDeceleration = 25f;
    private const float c_InverseOneEighty = 1f / 180f;
    private const float c_AirborneTurnSpeedProportion = 5.4f;
    private const float c_JumpAbortSpeed = 10f;

    private bool IsMoveInput => !Mathf.Approximately(_playerInput.MoveInput.sqrMagnitude, 0f);

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<ESPlayerInput>();

        _instance = this;
    }

    private void Reset()
    {
        MaxForwardSpeed = 1;
    }

    private void FixedUpdate()
    {
        _CalculateForwardMovement();
        _CalculateVerticalMovement();
        
        _SetTargetRotation();
        
        if (IsMoveInput)
            UpdateOrientation();

        _MoveCharacter();
    }

    void _MoveCharacter()
    {
        Vector3 movement = transform.position;

        if (_isGrounded)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position + Vector3.up * (c_groundedRayDistance * 0.5f), -Vector3.up);
            if (Physics.Raycast(ray, out hit, c_groundedRayDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                //Debug.Log("Ground");
                movement = Vector3.ProjectOnPlane(_forwardSpeed * transform.forward * Time.deltaTime, hit.normal);
            }
            else
            {

                movement = transform.forward * _forwardSpeed * Time.deltaTime;
            }
        }
        else
        {
            movement = transform.forward * (_forwardSpeed * Time.deltaTime);
        }

        //_characterController.transform.Rotate(_targetRotation);
       // _characterController.transform.rotation *= _targetRotation;
        //_characterController.transform.rotation *= deltaRotation;

        movement += Vector3.up * (_verticalSpeed * Time.deltaTime);

        _characterController.Move(movement);

        _isGrounded = _characterController.isGrounded;
    }

    void _CalculateForwardMovement()
    {
        Vector2 moveInput = _playerInput.MoveInput;
        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput.Normalize();
        }

        _desiredForwardSpeed = moveInput.magnitude * MaxForwardSpeed;
        float acceleration = IsMoveInput ? c_groundAcceleration : c_groundDeceleration;
        _forwardSpeed = Mathf.MoveTowards(_forwardSpeed, _desiredForwardSpeed, acceleration * Time.deltaTime);
    }

    void _CalculateVerticalMovement()
    {
        if (!_playerInput.JumpInput && _isGrounded)
            _isJumpable = true;
        
        if (_isGrounded)
        {
            _verticalSpeed = -Gravity * c_stickingGravityProportion;

            if (_playerInput.JumpInput && _isJumpable)
            {
                _verticalSpeed = JumpSpeed;
                _isGrounded = false;
                _isJumpable = false;
            }
        }
        else // Airborne
        {
            if (!_playerInput.JumpInput && _verticalSpeed > 0.0f)
            {
                _verticalSpeed -= c_JumpAbortSpeed * Time.deltaTime;
            }
            
            if (Mathf.Approximately(_verticalSpeed, 0f))
            {
                _verticalSpeed = 0f;
            }

            // Apply gravity
            _verticalSpeed -= Gravity * Time.deltaTime;
        }
    }

    void _SetTargetRotation()
    {
        Vector2 moveInput = _playerInput.MoveInput;
        Vector3 localMovementDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        Vector3 forward = Quaternion.Euler(0f, cameraSettings.keyboardAndMouseCamera.m_XAxis.Value, 0f) * Vector3.forward;
        forward.y = 0f;
        forward.Normalize();

        Quaternion targetRotation;
        
        // 1. 카메라 보는 방향, 입력값 방향 구하기
        // 2. 내적
        // 3. -1 이면 반대방향
        if (Mathf.Approximately(Vector3.Dot(localMovementDirection, Vector3.forward), -1.0f))
        {
            targetRotation = Quaternion.LookRotation(-forward);
        }
        else
        {
            Quaternion cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, localMovementDirection);
            targetRotation = Quaternion.LookRotation(cameraToInputOffset * forward);
        }

        Vector3 resultingForward = targetRotation * Vector3.forward;
        
        float angleCurrent = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float targetAngle = Mathf.Atan2(resultingForward.x, resultingForward.z) * Mathf.Rad2Deg;
        
        _angleDiff = Mathf.DeltaAngle(angleCurrent, targetAngle);
        _targetRotation = targetRotation;
    }

    void UpdateOrientation()
    {
        Vector3 localInput = new Vector3(_playerInput.MoveInput.x, 0f, _playerInput.MoveInput.y);
        float groundedTurnSpeed = Mathf.Lerp(MaxTurnSpeed, MinTurnSpeed, _forwardSpeed / _desiredForwardSpeed);
        float actualTurnSpeed = _isGrounded ? groundedTurnSpeed : Vector3.Angle(transform.forward, localInput) * c_InverseOneEighty * c_AirborneTurnSpeedProportion * groundedTurnSpeed;
        _targetRotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, actualTurnSpeed * Time.deltaTime);
        
        transform.rotation = _targetRotation;
    }
    

    public static ESPlayerMoveController Instance => _instance;
}
