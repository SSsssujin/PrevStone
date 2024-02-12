using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    private int _level;
    private int _skillPoint;

    // 발사간격
    private float _attackInterval = 0.12f;
    private float _lastAttackTime;
    
    private ESPlayerInput _playerInput;
    private ESPlayerMoveController _playerMoveController;
    
    private Camera _camera;

    private bool _canAttack => Time.time >= _lastAttackTime + _attackInterval;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _camera = Camera.main;
        _playerInput = GetComponent<ESPlayerInput>();
        _playerMoveController = GetComponent<ESPlayerMoveController>();
        Skills.AddSkill<ProjectileSkill>(this, transform.position);
        
        return true;
    }

    protected override void _UpdateController()
    {
        if (_playerInput.IsFire && _canAttack)
        {
            // Create bullet
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.transform.localScale = Vector3.one * 0.1f;
            bullet.transform.position = transform.position;
            var ps = bullet.DemandComponent<ProjectileSkill>();
            ps.Init();

            // Get Direction
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 shootingDirection;
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, ~LayerMask.GetMask("Player")))
            {
                Vector3 targetPoint = hit.point;
                shootingDirection = (targetPoint - transform.position).normalized;
            }
            else
            {
                shootingDirection = ray.direction;
            }
            ps.SetInfo(0, this, shootingDirection);
            
            _lastAttackTime = Time.time;
            
            // 위 코드 여기로 옮기기
            // 스킬셋 어케 관리할지 고민해보기
            //Skills.Skills[0].ActivateSkill();
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}
