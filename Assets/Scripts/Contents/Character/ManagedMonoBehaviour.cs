using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedMonoBehaviour : MonoBehaviour
{
    public ObjectType ObjectType { get; protected set; }
    
    private bool _init = false;
    
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        BoxManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        BoxManager.Instance.Unregister(this);
    }

    public virtual bool Init()
    {
        if (_init)
            return false;
        
        return true;
    }

    public void ManagedUpdate()
    {
        _UpdateController();
    }

    protected virtual void _UpdateController()
    {
        
    }
}
