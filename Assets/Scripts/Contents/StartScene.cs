using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class StartScene : IGameScene
{
    public override void Initialize()
    {
        Managers.Resource.LoadAllAsync<Object>("Start", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                _StartLoad();
            }
        });
    }

    private void _StartLoad()
    {
        
    }

    // Tester
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            FindObjectOfType<ESSceneTransitionPoint>().Transition();
            
    }
}
