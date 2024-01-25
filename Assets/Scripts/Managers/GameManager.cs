using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public ESSceneController GameSceneController => _gameSceneController;
    
    private ESSceneController _gameSceneController;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // 이거 비동기로 구현해줘
    public void SetupNewScene()
    {
        FindObjectOfType<IGameScene>().Initialize();
    }
}
