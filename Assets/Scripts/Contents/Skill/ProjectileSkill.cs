using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : SkillBase
{
    private CharacterBase _owner;
    
    private Vector3 _moveDir;
    private float _speed = 100f;
    private float _lifeTime = 3.0f;

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
}
