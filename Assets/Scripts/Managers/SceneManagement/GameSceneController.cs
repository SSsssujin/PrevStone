using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Gamekit3D.TransitionPoint;
using SceneTag = GameSceneDestination.SceneTag;

public class GameSceneController : MonoBehaviour
{
    public void Initialize()
    {
        
    }

    private bool _isTransitioning;

    private Scene _currentScene;
    private SceneTag _destinationTag;

    private GameSceneDestination GetDestination(SceneTag destinationTag)
    {
        GameSceneDestination[] entrances = FindObjectsOfType<GameSceneDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].Tag == destinationTag)
                return entrances[i];
        }
        Debug.LogWarning("No entrance was found with the " + destinationTag + " tag.");
        return null;
    }

    private void _SetEnteringGameObjectLocation(GameSceneDestination entrance)
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

    private IEnumerator _cSceneTransition(string newSceneName, SceneTag tag)
    {
        _isTransitioning = true;

        // 1. Save data

        // 2. Block player input
        ESPlayerInput.Instance?.ReleaseControl();

        // 3. Fade scene
        yield return SceneFader.Instance.Fade();

        // 4. Load new scene -> 현재 string, 나중에 어드레서블로 바꿀것
        yield return SceneManager.LoadSceneAsync(newSceneName);

        // Block new player input

        // 5. Start new scene
        GameSceneDestination entrance = GetDestination(tag);
        _SetEnteringGameObjectLocation(entrance);
        //_SetupNewScene(transitionType, entrance);
        entrance.OnReachDestination?.Invoke();

        _isTransitioning = false;
    }

    private void _SetupNewScene()
    {

    }

    private void _SetZoneStart(GameSceneDestination entrance)
    {
        _currentScene = entrance.gameObject.scene;
        _destinationTag = entrance.Tag;
    }

}
