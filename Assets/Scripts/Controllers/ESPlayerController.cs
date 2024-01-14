using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Rendering.HybridV2;
using UnityEngine;

[RequireComponent(typeof(ESPlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class ESPlayerController : MonoBehaviour
{
    private static ESPlayerController _instance;

    private float Gravity = 20;
    private float JumpSpeed = 10f;
    private float MaxForwardSpeed = 5f;

    public PlayerCameraSettings cameraSettings;

    private CharacterController _characterController;
    private ESPlayerInput _playerInput;

    private bool _isGrounded;
    private float _verticalSpeed;
    private float _forwardSpeed;
    private float _desiredForwardSpeed;

    private const float _stickingGravityProportion = 0.3f;
    private const float _groundedRayDistance = 1f;
    private const float _groundAcceleration = 20f;
    private const float _groundDeceleration = 25f;

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
        CalculateForwardMovement();
        CalculateVerticalMovement();

        _MoveCharacter();
    }

    Vector3 deltaPosition, previousPosition;

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

    void CalculateForwardMovement()
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

    void CalculateVerticalMovement()
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


    public static ESPlayerController Instance => _instance;
}
