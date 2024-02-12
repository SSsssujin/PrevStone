using System;

public class CharacterBase : ManagedMonoBehaviour
{
    protected float _speed;
    
    public int Hp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;

    protected float _attackPower;
    protected float _attackSpeed;
    
    public SkillBook Skills { get; protected set; }

    public event Action OnDeath;
    
    public override bool Init()
    {
        base.Init();

        Skills = gameObject.DemandComponent<SkillBook>();

        return true;
    }

    protected virtual void _OnDamaged(ManagedMonoBehaviour attacker, int damage)
    {
        if (Hp <= 0)
            return;
        
        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            _OnDead();
        }
    }
    
    protected virtual void _OnDead()
    {
        OnDeath?.Invoke();   
    }
}
