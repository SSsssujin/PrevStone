using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SkillBase : ManagedMonoBehaviour
{
    class SkillData
    {
        public int TemplateID;
        public string Prefab;
        public int Damage;
    }
    
    public CharacterBase Owner { get; set; }
    
    private Coroutine _coDestroy;
    private Coroutine _coSkill;
    
    public virtual void ActivateSkill() { }
    
    protected abstract void DoSkillJob();

    public void _GenerateProjectile(CharacterBase owner, Vector3 startPos, Vector3 dir)
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.localScale = Vector3.one * 0.1f;
        bullet.transform.position = startPos;
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
        ps.SetInfo(0, owner, shootingDirection);

        //GameObject go = Managers.Resource.Instantiate("")
        //ProjectileSkill ps = Managers.Object.Spawn<ProjectileSkill>();
    }
    
    protected void _StartDestroy(float delaySecond)
    {
        _StopDestroy();
        _coDestroy = StartCoroutine(CoDestroy(delaySecond));
    }

    protected void _StopDestroy()
    {
        if (_coDestroy != null)
        {
            StopCoroutine(_coDestroy);
            _coDestroy = null;
        }
    }
    
    IEnumerator CoDestroy(float delaySecond)
    {
        yield return new WaitForSeconds(delaySecond);
        if (this.IsValid())
        {
            Destroy(gameObject);
            //Managers.Object.Despawn(bullet);
        }
    }
}
