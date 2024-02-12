using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-150)]
public class BoxManager : MonoBehaviour
{
    public static BoxManager Instance { get; private set; }

    public List<ManagedMonoBehaviour> _managedBoxes = new List<ManagedMonoBehaviour>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // update objects here
        for (int i = 0; i < _managedBoxes.Count; ++i)
        {
            _managedBoxes[i].ManagedUpdate();
        }
    }

    public void Register(ManagedMonoBehaviour box)
    {
        _managedBoxes.Add(box);
    }

    public void Unregister(ManagedMonoBehaviour box)
    {
        _managedBoxes.Remove(box);
    }
}
