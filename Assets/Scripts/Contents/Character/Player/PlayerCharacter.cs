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

    private ESPlayerController _playerController;
    // 밑에 있는 애들 PlayerController 통해서 다시 캐싱하기
    private ESPlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    
    private Camera _camera;

    private bool _canAttack => Time.time >= _lastAttackTime + _attackInterval;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Cursor.lockState = CursorLockMode.Locked;
        
        _camera = Camera.main;
        _playerInput = GetComponent<ESPlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        Skills.AddSkill<ProjectileSkill>(this, transform.position);
        
        return true;
    }

    public ESPlayerCameraSettings CameraSettings;
    
    protected override void _UpdateController()
    {
        if (_playerInput.IsFire && _canAttack)
        {
            // Create bullet
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.transform.localScale = Vector3.one * 0.1f;
            bullet.transform.position = transform.position;
            bullet.DemandComponent<Collider>().isTrigger = true;
            var ps = bullet.DemandComponent<ProjectileSkill>();
            ps.Init();

            // 충돌 정보 저장용 컨테이너
            RaycastHit hit;

            // 탄알이 맞은 위치
            // Vector3 hitPosition = Vector3.zero;
            //
            // if (Physics.Raycast(transform.position, transform.forward, out hit, fireDistance))
            // {
            //     // 레이가 어떤 물체와 충돌 시
            //     IDamageable target = hit.collider.GetComponent<IDamageable>();
            //
            //     if (target != null)
            //     {
            //         target.OnDamage(damage, hit.point, hit.normal);
            //     }
            //
            //     hitPosition = hit.point;
            // }
            // else
            // {
            //     // 충돌 X 시
            //     // 충돌 포지션 == 탄알이 최대 사정거리까지 날아갔을 때의 위치
            //     hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
            // }
            //
            // Vector3 forward = Quaternion.Euler(0f, CameraSettings.keyboardAndMouseCamera.m_XAxis.Value, 0f) * Vector3.forward;
            // forward.y = 0f;
            // forward.Normalize();
            //
            // // Get Direction
            // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ps.SetInfo(0, this,transform.forward);
            
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
