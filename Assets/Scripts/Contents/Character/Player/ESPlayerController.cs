using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(ESPlayerInput),typeof(PlayerMovement), typeof(PlayerCharacter))]
public class ESPlayerController : ManagedMonoBehaviour
{
    private static ESPlayerController _instance;

    // Temp
    [SerializeField]
    private List<PlayerCharacterData> _playerParty;

    [Header("Listen to Event Channels")] 
    [SerializeField]
    private VoidEventChannelSO OnSceneLoadComplete;

    private PlayerMovement _playerMovement;
    private ESPlayerInput _playerInput;
    private PlayerCharacter _playerCharacter;

    private int _skillPoint;

    private void Start()
    {
        OnSceneLoadComplete.OnEventRaised += () => _UpdatePlayerCharacter(1);
        OnSceneLoadComplete.OnEventRaised += _FindPlayerPosition;
        _Initialize();
    }

    private void Reset()
    {
        _playerCharacter = GetComponent<PlayerCharacter>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<ESPlayerInput>();
    }

    private void _Initialize()
    {
        
    }

    public void ResetPlayerParty()
    {
        
    }

    private void _FindPlayerPosition()
    {
        transform.position = GameObject.Find("Position").transform.Find("Player").position;
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
        _playerCharacter ??= GetComponent<PlayerCharacter>();

        int playerIndex = pressedNum - 1;

        if (_playerParty.IndexOf(_playerCharacter._playerData) == playerIndex)
        {
            Debug.Log("Same Character summoned already");
            return;
        }
        
        _playerCharacter.UpdatePlayerData(_playerParty[playerIndex]);
    }

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
