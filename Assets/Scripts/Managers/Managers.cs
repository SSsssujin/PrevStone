using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    private static bool _init;

    private GameManager _game;

    private ObjectManager _object = new();
    private PoolManager _pool = new();
    private ResourceManager _resource = new();

    private void Awake()
    {
       // Game.Initialize();
    }

    public static GameManager Game => Instance._game;

    public static ObjectManager Object => Instance._object; 
    public static PoolManager Pool => Instance._pool; 
    public static ResourceManager Resource => Instance._resource; 
    
    public static Managers Instance
    {
        get
        {
            if (_init == false)
            {
                GameObject go = GameObject.Find("Managers");
                if (go == null)
                {
                    go = new GameObject() { name = "Managers" };
                    go.DemandComponent<Managers>();
                }
                
                DontDestroyOnLoad(go);
                _instance = go.DemandComponent<Managers>();
                _init = true;
            }

            return _instance;
        }
    }
}
