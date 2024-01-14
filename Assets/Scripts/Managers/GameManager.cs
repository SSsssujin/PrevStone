using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ESSceneController _gameSceneController;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
