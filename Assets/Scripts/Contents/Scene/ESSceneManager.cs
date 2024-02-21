using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESSceneManager : Singleton<ESSceneManager>
{
    [Header("Broadcast on Event Channels")]
    [SerializeField] private VoidEventChannelSO OnSceneLoadComplete;
    [SerializeField] private SceneInfoCollection _sceneList;

    protected override void Awake()
    {
        
    }
    
    
    private void _Initialize()
    {
        
    }

    private IEnumerator _cSetupNewScene()
    {

        yield return null;
        OnSceneLoadComplete?.RaiseEvent();
    }

    private SceneInfo _GetNextScene()
    {
        //SceneInfo 
        return new SceneInfo();
    }

    private string _GetSceneName() => SceneManager.GetActiveScene().name;
}
