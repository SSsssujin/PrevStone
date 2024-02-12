using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacter : CharacterBase
{
    protected override void _OnDead()
    {
        _DropLoot(); 
    }

    private void _DropLoot()
    {
        
    }
}
