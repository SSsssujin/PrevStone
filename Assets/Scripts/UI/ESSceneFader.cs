using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ESSceneFader : MonoBehaviour
{
    private static ESSceneFader _instance;

    private Image _fadeImg;

    private void Start()
    {
        _fadeImg = GetComponent<Image>();

        _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public YieldInstruction Fade(bool isFadeIn)
    {
        _fadeImg.gameObject.SetActive(true);
        Color color = _fadeImg.color;
        color.a = isFadeIn ? 1 : 0;
        _fadeImg.color = color;
        var fadeTweener = _fadeImg.DOFade(isFadeIn ? 0 : 1, 1f);
        return fadeTweener.WaitForCompletion();
    }

    public static ESSceneFader Instance
    {
        get
        {
            _instance ??= FindObjectOfType<ESSceneFader>();
            return _instance;
        }
    }
}
