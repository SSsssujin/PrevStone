using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESSceneTransitionPoint : MonoBehaviour
{
    // Receiver 하나 만들어서
    // 거기서 쭉 진행되도록?

    public enum TransitionType
    {
        DifferentZone, DifferentNonGameplayScene, SameScene,
    }


    public enum TransitionWhen
    {
        ExternalCall, OnTriggerEnter,
    }

    private void Update()
    {
        // Tester
        if (Input.GetKeyDown(KeyCode.T))
            Transition();
    }

    [SceneName]
    public string NewScene;

    public void Transition()
    {
        ESSceneController.TransitionToScene(this);
    }
}
