using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneTag = ESSceneTransitionDestination.SceneTag;

public class ESSceneController : MonoBehaviour
{
    private static ESSceneController _instance;

    public static ESSceneController Instance
    {
        get { return _instance ?? Create(); }
    }

    public static ESSceneController Create()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        _instance = sceneControllerGameObject.AddComponent<ESSceneController>();

        return _instance;
    }

    private bool _isTransitioning;

    private Scene _currentScene;
    private SceneTag _destinationTag;

    public static void TransitionToScene(ESSceneTransitionPoint transitionPoint)
    {
        Debug.Log(Instance == null);
        Instance.StartCoroutine(Instance._cSceneTransition(transitionPoint.NewScene)); //, transitionPoint.transitionDestinationTag));
    }

    private ESSceneTransitionDestination GetDestination(SceneTag destinationTag)
    {
        ESSceneTransitionDestination[] entrances = FindObjectsOfType<ESSceneTransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].Tag == destinationTag)
                return entrances[i];
        }
        Debug.LogWarning("No entrance was found with the " + destinationTag + " tag.");
        return null;
    }

    private void _SetEnteringGameObjectLocation(ESSceneTransitionDestination entrance)
    {
        if (entrance == null)
        {
            Debug.LogWarning("Entering Transform's location has not been set.");
            return;
        }
        entrance.TransitioningGameObject.LoadAssetAsync<GameObject>().Completed += op =>
        {
            Transform entranceLocation = entrance.transform;
            Transform enteringTransform = op.Result.transform;
            enteringTransform.position = entranceLocation.position;
            enteringTransform.rotation = entranceLocation.rotation;
        };
    }

    private IEnumerator _cSceneTransition(string newSceneName, SceneTag tag = SceneTag.A)
    {
        _isTransitioning = true;

        // 1. Save data

        // 2. Block player input
        //ESPlayerInput.Instance?.ReleaseControl();

        // 3. Fade scene
        Debug.Log("Fade start");
        yield return ESSceneFader.Instance.Fade(false);

        // 4. Load new scene -> 현재 string, 나중에 어드레서블로 바꿀것
        yield return SceneManager.LoadSceneAsync(newSceneName);

        // Block new player input
        GameManager.Instance.SetupNewScene();

        // 5. Start new scene
        ESSceneTransitionDestination entrance = GetDestination(tag);
        _SetEnteringGameObjectLocation(entrance);
        _SetupNewScene(/* type */ entrance);
        entrance.OnReachDestination?.Invoke();

        // Fade scene
        //yield return ESSceneFader.Instance.Fade(true);
        Debug.Log("Fade end");
        
        // 새로 찾아야됨
        //ESPlayerInput.Instance?.GainControl();

        _isTransitioning = false;
    }

    private void _SetupNewScene(ESSceneTransitionDestination entrance)
    {
        _SetZoneStart(entrance);
    }

    private void _SetZoneStart(ESSceneTransitionDestination entrance)
    {
        _currentScene = entrance.gameObject.scene;
        _destinationTag = entrance.Tag;
    }
}
