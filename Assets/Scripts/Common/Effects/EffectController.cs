using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    IEffect[] effects;

    private void Awake()
    {
        effects = GetComponentsInChildren<IEffect>();
    }

    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        foreach (IEffect effect in effects)
        {
            effect.Show();
        }
    }

    public void Hide()
    {
        foreach (IEffect effect in effects)
        {
            effect.Hide();
        }
    }
}
