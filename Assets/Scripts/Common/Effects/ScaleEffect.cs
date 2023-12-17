using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class ScaleEffect : TTMonoBehaviour, IEffect
{
    [SerializeField] protected float scaleRate;

    Vector3 originScale;

    protected virtual void Awake()
    {
        originScale = transform.localScale;
    }

    public void Show()
    {
        transform.localScale = originScale * scaleRate;
        this.ScalceTo(originScale);
    }

    public void Hide()
    {
        this.ScalceTo(originScale * scaleRate, () => { gameObject.SetActive(false); });
    }

}
