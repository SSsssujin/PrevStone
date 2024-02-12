using System.Collections;
using System.Collections.Generic;
using Gamekit3D;
using UnityEngine;

public class StoneProjectile : ProjectileSkill
{
    public GameObject bulletPrefab; // 탄환 프리팹
    public Transform bulletSpawnPoint; 
    
    protected override void DoSkillJob()
    {
        var player = FindAnyObjectByType<PlayerController>();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 클릭 위치로 레이 생성
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) // 레이와 충돌한 객체가 있는지 검사
        {
            Vector3 targetPoint = hit.point; // 충돌 지점을 타겟 포인트로 설정
            Vector3 direction = targetPoint - bulletSpawnPoint.position; // 발사 방향 계산
            _GenerateProjectile(null, player.transform.position, direction.normalized);
        }
        
    }
}
