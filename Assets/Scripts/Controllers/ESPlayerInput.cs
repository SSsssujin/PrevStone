using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESPlayerInput : MonoBehaviour
{
    private bool _isJump;

    private Vector2 _movement;

    private void Update()
    {
        _movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        _isJump = Input.GetButton("Jump");
    }

    public Vector2 MoveInput => _movement;
    
    public bool JumpInput => _isJump;
}
