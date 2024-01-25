using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ESSceneTransitionDestination : MonoBehaviour
{
    public enum SceneTag { A, B, C, D, E, F }

    public SceneTag Tag;
    public AssetReferenceGameObject TransitioningGameObject;
    public Action OnReachDestination;
    
    //public IGameScene GameScene => FindObjectOfType<IGameScene>();
}
