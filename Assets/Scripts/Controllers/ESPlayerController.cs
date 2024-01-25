using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ESPlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class ESPlayerController : MonoBehaviour
{
    private static ESPlayerController _instance;

    public float Gravity = 20;
    public float JumpSpeed = 10f;
    public float MaxForwardSpeed = 2.5f;
    public float MinTurnSpeed = 400f;      
    public float MaxTurnSpeed = 1200f;   

    public ESPlayerCameraSettings cameraSettings;

    private CharacterController _characterController;
    private ESPlayerInput _playerInput;

    private bool _isGrounded;
    private float _verticalSpeed;
    private float _forwardSpeed;
    private float _desiredForwardSpeed;

    private Quaternion _targetRotation;
    private float _angleDiff;    

    private const float _stickingGravityProportion = 0.3f;
    private const float _groundedRayDistance = 1f;
    private const float _groundAcceleration = 20f;
    private const float _groundDeceleration = 25f;
    const float c_InverseOneEighty = 1f / 180f;
    const float c_AirborneTurnSpeedProportion = 5.4f;

    Vector3 deltaPosition, previousPosition;

    private bool IsMoveInput => !Mathf.Approximately(_playerInput.MoveInput.sqrMagnitude, 0f);

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<ESPlayerInput>();

        _instance = this;
    }

    private void Start()
    {
        previousPosition = transform.position;
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
        Vector3 movement;
        Vector3 move = new Vector3(_playerInput.MoveInput.x,0, _playerInput.MoveInput.y);

        deltaPosition = transform.position - previousPosition;

        if (_isGrounded)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position + Vector3.up * _groundedRayDistance * 0.5f, -Vector3.up);
            if (Physics.Raycast(ray, out hit, _groundedRayDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("grounded");
                movement = Vector3.ProjectOnPlane(deltaPosition, hit.normal);
            }
            else
            {
                
                movement = move;
            }
        }
        else
        {
            movement = _forwardSpeed * transform.forward * Time.deltaTime;
        }

        movement += _verticalSpeed * Vector3.up * Time.deltaTime;

        previousPosition = transform.position;

        _characterController.Move(movement);

        _isGrounded = _characterController.isGrounded;
    }

    void _CalculateForwardMovement()
    {
        Vector2 moveInput = _playerInput.MoveInput;
        if (moveInput.sqrMagnitude > 1f)
        {
            Debug.LogError(moveInput);
            moveInput.Normalize();
        }

        _desiredForwardSpeed = moveInput.magnitude * MaxForwardSpeed;

        float acceleration = IsMoveInput ? _groundAcceleration : _groundDeceleration;

        _forwardSpeed = Mathf.MoveTowards(_forwardSpeed, _desiredForwardSpeed, acceleration * Time.deltaTime);
    }

    void _CalculateVerticalMovement()
    {
        if (_isGrounded)
        {
            _verticalSpeed = -Gravity * _stickingGravityProportion;

            if (_playerInput.JumpInput)
            {
                _verticalSpeed = JumpSpeed;
                _isGrounded = false;
            }
        }
        else
        {
            if (!_playerInput.JumpInput && _verticalSpeed > 0.0f)
            {
                //_verticalSpeed -= 
            }

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
            
        // If the local movement direction is the opposite of forward then the target rotation should be towards the camera.
        if (Mathf.Approximately(Vector3.Dot(localMovementDirection, Vector3.forward), -1.0f))
        {
            targetRotation = Quaternion.LookRotation(-forward);
        }
        else
        {
            // Otherwise the rotation should be the offset of the input from the camera's forward.
            Quaternion cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, localMovementDirection);
            targetRotation = Quaternion.LookRotation(cameraToInputOffset * forward);
        }

        Vector3 resultingForward = targetRotation * Vector3.forward;
        
        // Find the difference between the current rotation of the player and the desired rotation of the player in radians.
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
    

    public static ESPlayerController Instance => _instance;
}
