using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ESPlayerController : ManagedMonoBehaviour
{
    private static ESPlayerController _instance;

    public static ESPlayerController Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = GameObject.Find("Player").GetComponent<ESPlayerController>();
            }
            return _instance;
        }
    }
    
    public PlayerCharacter PlayerCharacter;
    public PlayerMovement PlayerMovement;
    public ESPlayerInput PlayerInput;

    private void Reset()
    {
        PlayerCharacter = GetComponent<PlayerCharacter>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerInput = GetComponent<ESPlayerInput>();
    }

    protected override void _UpdateController()
    {
        
    }

    private void _UpdatePlayerCharacter(int pressedNum)
    {
        
    }

    public Action<int> OnAlphaNumPressed;
}
