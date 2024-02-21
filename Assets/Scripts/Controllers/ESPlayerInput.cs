using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESPlayerInput : MonoBehaviour
{
    private int _selectedNum;
    private bool _isJump;
    private bool _isFire;

    private bool _isExternalInputBlocked;
    private bool _isPlayerInputBlocked;

    private Vector2 _movement;

    private void Update()
    {
        _movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _isJump = Input.GetButton("Jump");
        _isFire = Input.GetButton("Fire1");
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

    public bool IsFire => _isFire;
}
