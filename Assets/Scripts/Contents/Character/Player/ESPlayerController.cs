using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ESPlayerController : ManagedMonoBehaviour
{
    private static ESPlayerController _instance;

    [SerializeField]
    private List<PlayerCharacterData> _playerParty;
    private PlayerCharacterData _prevPlayer, _curPlayer;
    
    private PlayerCharacter _playerCharacter;
    private PlayerMovement _playerMovement;
    private ESPlayerInput _playerInput;

    private int _skillPoint;


    private void Start()
    {
        _UpdatePlayerCharacter(1);
    }

    private void Reset()
    {
        _playerCharacter = GetComponent<PlayerCharacter>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<ESPlayerInput>();
    }

    public void ResetPlayerParty()
    {
        
    }

    protected override void _UpdateController()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _UpdatePlayerCharacter(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            _UpdatePlayerCharacter(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            _UpdatePlayerCharacter(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            _UpdatePlayerCharacter(4);
    }

    private void _UpdatePlayerCharacter(int pressedNum)
    {
        if (_playerCharacter == null)
            _playerCharacter = GetComponent<PlayerCharacter>();
        
        _playerCharacter.UpdatePlayerData(_playerParty[pressedNum - 1]);
    }
    
    public Action<int> OnAlphaNumPressed;
    
    public PlayerCharacter PlayerCharacter => Instance._playerCharacter;
    public PlayerMovement PlayerMovement => Instance._playerMovement;
    public ESPlayerInput PlayerInput => Instance._playerInput;
    
    public static ESPlayerController Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = GameObject.Find("Player").DemandComponent<ESPlayerController>();
            }
            return _instance;
        }
    }
}
