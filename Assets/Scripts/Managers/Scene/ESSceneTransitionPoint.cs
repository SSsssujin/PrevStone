using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESSceneTransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        DifferentZone, DifferentNonGameplayScene, SameScene,
    }

    public enum TransitionWhen
    {
        ExternalCall, OnTriggerEnter,
    }

    [SceneName]
    public string NewScene;

    public void Transition()
    {
        //ESSceneController.TransitionToScene(this);
    }
}
