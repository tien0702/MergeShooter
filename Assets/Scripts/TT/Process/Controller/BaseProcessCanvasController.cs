using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TT.Process.Base;

namespace TT.Process.Controller
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public abstract class BaseProcessCanvasController : BaseProcess
    {
        [SerializeField] protected Image fill;
        protected virtual void Awake()
        {
            fill = GetComponent<Image>();
        }

        protected virtual void Display()
        {
            fill.fillAmount = currentValue / maxValue;
        }
    }
}
