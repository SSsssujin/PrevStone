using System;
using UnityEngine;

public class CharacterBase : ManagedMonoBehaviour, IDamageable
{
    //protected CharacterData CharacterData;
    
    protected float _speed;
    
    public int Hp { get; set; } = 30;
    public int MaxHp { get; set; } = 30;

    public float _attackPower;
    protected float _attackSpeed;
    
    public SkillBook Skills { get; protected set; }

    public event Action OnDeath;
    
    public override bool Init()
    {
        base.Init();

        Skills = gameObject.DemandComponent<SkillBook>();

        return true;
    }

    //protected virtual void _OnDamaged(ManagedMonoBehaviour attacker, int damage)
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (Hp <= 0)
            return;
        
        Hp -= (int)damage;

        if (Hp <= 0)
        {
            Hp = 0;
            _OnDead();
        }
        
        Debug.Log($"{gameObject}에게 {damage} 데미지 발생, 남은 HP : {Hp}");        
    }
    
    protected virtual void _OnDead()
    {
        OnDeath?.Invoke();   
        
        Managers.Object.Despawn<CharacterBase>(this);
    }
}
