using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Space]
    
    // Scene
    [Header("Scene Transition")]
    [SceneName] public string NextScene;
    private bool _isTransitioning;

    public void Initialize() { }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        Cursor.lockState = CursorLockMode.Locked;

        // Transition 조건 수정해주기
        StartCoroutine(nameof(_cSetUpNewScene));
    }

    private IEnumerator _cSetUpNewScene()
    {
        yield return null;

        // var gameSceneInfo = FindFirstObjectByType<GameScene>();
        //
        // // Load
        // //yield return SceneManager.LoadSceneAsync(gameSceneInfo.NextScene);
        //
        // // Initialize
        // gameSceneInfo.Initialize();
        // yield return new WaitUntil (() => gameSceneInfo.IsLoadComplete == true);
        // OnSceneLoadComplete?.RaiseEvent();
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
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(nameof(_cSetUpNewScene));
            }
        }
    }
}
