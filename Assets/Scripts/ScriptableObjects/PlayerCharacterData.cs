using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "ScriptableObjects/CharacterData/Player", fileName = "Player", order = 0)]
public class PlayerCharacterData : ScriptableObject
{
    public int TemplateID;
    public string Name;
    
    public GameObject Model;
    public int MaxHP;

    [Range(1, 5)] public float AttackPower;
    [Range(1, 5)] public float AttackSpeed;
    public float DefensePower;
    
    public float JumpPower;
    public float MoveSpeed;

    public List<SkillBase> Skiils;

    public void Init()
    {
        // Create Character model
        Model = Managers.Resource.Instantiate(Name);
        Model.transform.SetParent(GameObject.Find("Player").transform);
        Model.transform.localPosition = Vector3.zero;
        
        // Set Skills
    }

    public void Exit()
    {
        Destroy(Model);
    }

    private void _SetPlayerCharacterData()
    {
        
    }
}
