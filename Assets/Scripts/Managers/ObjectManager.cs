using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    // public T Spawn<T>(string key) where T : BoxManaged
    // {
    //     GameObject go = Managers.Resource.Instantiate(key);
    //     
    // }

    public void Despawn<T>(T obj) where T : ManagedMonoBehaviour
    {
        
        
        Managers.Resource.Destroy(obj.gameObject);
    }
}
