using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : SkillBase
{
    private CharacterBase _owner;
    
    private Vector3 _moveDir;
    private float _speed = 10f;
    private float _lifeTime = 10f;

    public override bool Init()
    {
        base.Init();
        
        _StartDestroy(_lifeTime);

        return true;
    }

    public override void ActivateSkill()
    {
        DoSkillJob();
    }

    public void SetInfo(int templateID, CharacterBase owner, Vector3 moveDir)
    {
        _owner = owner;
        _moveDir = moveDir;
    }

    protected override void _UpdateController()
    {
        transform.position += _moveDir * _speed * Time.deltaTime;
    }

    protected override void DoSkillJob()
    {
        _GenerateProjectile(_owner, _owner.transform.position, _moveDir);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponentInChildren<IDamageable>();
        target?.OnDamage(_owner._attackPower, transform.position, (transform.position - other.transform.position).normalized);
    }
}
