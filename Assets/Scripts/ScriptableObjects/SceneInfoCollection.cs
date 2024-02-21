using System;
using UnityEngine;

[Serializable]
public class SceneInfo
{
    [SerializeField] 
    private int level;
    [SerializeField]
    private string _sceneName;
    // 몬스터
    
    public string SceneName => _sceneName;
}

[CreateAssetMenu(menuName = "ScriptableObjects/Scene/SceneInfo", fileName = "Scene", order = 0)]
public class SceneInfoCollection : ScriptableObject
{
    public SceneInfo[] SceneInfoList;
}
