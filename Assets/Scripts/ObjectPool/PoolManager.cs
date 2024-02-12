using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

class Pool
{
    private GameObject _prefab;
    private IObjectPool<GameObject> _pool;

    private Transform _root;

    Transform Root
    {
        get
        {
            if (_root == null)
            {
                GameObject go = new GameObject() { name = $"{_prefab.name}_Root" };
                _root = go.transform;
            }
            return _root;
        }
    }

    public Pool(GameObject prefab, int count)
    {
        _prefab = prefab;
        _pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    public GameObject Pop()
    {
        return _pool.Get();
    }

    public void Push(GameObject go)
    {
        try
        {
            _pool.Release(go);
        }
        catch (Exception e)
        {
            Debug.LogError(e + " : " + go.name);
            go.name = "Error Obj";
        }
    }

    GameObject OnCreate()
    {
        GameObject go = GameObject.Instantiate(_prefab, Root, true);
        go.name = _prefab.name;
        return go;
    }
    
    void OnGet(GameObject go)
    {
        go.SetActive(true);
    }

    void OnRelease(GameObject go)
    {
        go.SetActive(false);
    }

    void OnDestroy(GameObject go)
    {
        GameObject.Destroy(go);
    }
}

public class PoolManager
{
    private static readonly Lazy<PoolManager> _instance = new ();
    private const int _poolDefaultCount = 10;
    private Dictionary<ePoolableType, Pool> _pools = new ();

    public GameObject Pop(ePoolableType type)
    {
        if (!_pools.ContainsKey(type))
            return null;

        return _pools[type].Pop();
    }

    public bool Push(GameObject go)
    {
        if (!go.TryGetComponent<PoolableType>(out var poolableObject) || 
            !_pools.ContainsKey(poolableObject.PoolType))
            return false;

        _pools[poolableObject.PoolType].Push(go);
        return true;
    }

    public void CreatePool(ePoolableType poolType, GameObject prefab, int count = _poolDefaultCount)
    {
        Pool pool = new Pool(prefab, count);
        _pools.Add(poolType, pool);
    }
    
    public static PoolManager Instance => _instance.Value;
}