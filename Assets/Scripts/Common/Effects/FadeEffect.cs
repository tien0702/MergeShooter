using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class FadeEffect : TTMonoBehaviour, IEffect
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            Destroy(this);
    }

    public void Show()
    {
        canvasGroup.alpha = 0;
        LeanTween.alphaCanvas(canvasGroup, 1f, time);
    }

    public void Hide()
    {
        LeanTween.alphaCanvas(canvasGroup, 0, time);
    }
}
