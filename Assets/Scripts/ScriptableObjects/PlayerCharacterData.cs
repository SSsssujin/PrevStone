using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character Data", order = 0)]
public class PlayerCharacterData : ScriptableObject
{
    public int TemplateID;
    public string Name;

    public GameObject Model;
    public int MaxHP;
    
    [Range(1, 5)]
    public float AttackPower;
    [Range(1, 5)]
    public float AttackSpeed;
    public float DefensePower;
    
    public float JumpPower;
    public float MoveSpeed;

    public List<SkillBase> Skiils;

    public void Init()
    {
        Debug.Log(Name);   
        
        // Set Skills
    }

    public void Exit()
    {
    }

    private void SetPlayerCharacterData()
    {
        
    }

}
