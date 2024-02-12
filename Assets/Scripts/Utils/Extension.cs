using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static T DemandComponent<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
    
    public static bool IsValid(this ManagedMonoBehaviour bc)
    {
        return bc != null && bc.isActiveAndEnabled;
    }
}
