using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character Data", order = 0)]
public class CharacterData : ScriptableObject
{
    public int TemplateID;
    public int MaxHP;
    
}
