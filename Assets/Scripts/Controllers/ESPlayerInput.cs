using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESPlayerInput : MonoBehaviour
{
    private static ESPlayerInput _instance;
    public static ESPlayerInput Instance => _instance;

    private bool _isJump;

    private bool _isExternalInputBlocked;
    private bool _isPlayerInputBlocked;

    private Vector2 _movement;

    private void Awake()
    {
        _instance = this;
    }


    private void Update()
    {
        _movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        _isJump = Input.GetButton("Jump");
    }

    public void ReleaseControl()
    {
        _isExternalInputBlocked = true;
    }

    public void GainControl()
    {
        _isExternalInputBlocked = false;
    }

    public Vector2 MoveInput => _isExternalInputBlocked || _isPlayerInputBlocked ? Vector2.zero : _movement;
    
    public bool JumpInput => _isJump;
}
