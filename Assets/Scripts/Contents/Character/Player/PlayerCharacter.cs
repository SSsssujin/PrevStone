using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    private int _level;

    private ESPlayerController _playerController;

    public PlayerCharacterData _playerData;
    
    // 밑에 있는 애들 PlayerController 통해서 다시 캐싱하기
    private ESPlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        //ESPlayerController.Instance.OnAlphaNumPressed += 
        
        _playerInput ??= GetComponent<ESPlayerInput>();
        _playerMovement ??= GetComponent<PlayerMovement>();
        
        //Skills.AddSkill<ProjectileSkill>(this, transform.position);
        
        return true;
    }

    public ESPlayerCameraSettings CameraSettings;
    
    protected override void _UpdateController()
    {
        if (_playerInput.IsFire && _canAttack)
        {
            _Shot();
        }
    }

    public void UpdatePlayerData(PlayerCharacterData data)
    {
        Debug.Log("Update Player Data");
        
        _playerData?.Exit();
            
        _playerData = data;
        _playerData.Init();
    }

        
    // 발사간격
    private float _attackInterval = 0.12f;
    private float _lastAttackTime;
    
    private bool _canAttack => Time.time >= _lastAttackTime + _attackInterval;
    
    private void _Shot()
    {
        // Create bullet
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.localScale = Vector3.one * 0.1f;
        bullet.transform.position = transform.position;
        bullet.DemandComponent<Collider>().isTrigger = true;
        var ps = bullet.DemandComponent<ProjectileSkill>();
        ps.Init();
        ps.SetInfo(0, this,transform.forward);
        _lastAttackTime = Time.time;
            
        // 위 코드 여기로 옮기기
        // 스킬셋 어케 관리할지 고민해보기
        //Skills.Skills[0].ActivateSkill();
    }

    public void Exit()
    {
        StartCoroutine(_cExit());

        IEnumerator _cExit()
        {
            yield return null;
            Destroy(this);
        }
    }
}
