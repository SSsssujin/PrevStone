using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    private static SceneFader _instance;

    public Coroutine CorFadeScene;

    private YieldInstruction CorTest;

    private void Start()
    {
        CorFadeScene = StartCoroutine(cFade());
    }

    private IEnumerator cFade()
    {
        yield return new WaitForSeconds(1);
    }

    public YieldInstruction Fade()
    {
        return new WaitForSeconds(1);
    }

    public static SceneFader Instance => _instance;
}
