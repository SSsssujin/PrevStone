using System;
using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController PlayerController;
    
    public ESSceneController GameSceneController => _gameSceneController;
    
    private ESSceneController _gameSceneController;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // 이거 비동기로 구현해줘
    public void SetupNewScene()
    {
        FindObjectOfType<IGameScene>().Initialize();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
