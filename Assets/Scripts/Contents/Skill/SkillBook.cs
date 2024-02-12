using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    public List<SkillBase> Skills = new();

    public T AddSkill<T>(CharacterBase owner, Vector3 position, Transform parent = null) where T : SkillBase
    {
        //if (typeof(T) == typeof(ProjectileSkill))
        {
            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ProjectileSkill skill = GameObject.CreatePrimitive(PrimitiveType.Sphere).DemandComponent<ProjectileSkill>();
            skill.SetInfo(0, owner, position);
            Skills.Add(skill);
            return skill as T;
        }
        
        // Temp
        //ProjectileSkill skill = gameObject.DemandComponent<ProjectileSkill>();//Managers.Object.Spawn<T>();
        //return skill as T;
    }
}
