using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCondition : MonoBehaviour, ICondition
{
    protected bool isSuitable = false;
    protected Action<ICondition> onSuitable;
    public virtual bool IsSuitable => isSuitable;

    public virtual Action<ICondition> OnSuitable
    {
        set
        {
            onSuitable = value;
        }
    }

    protected virtual void SetSuitableCondition(bool suiable)
    {
        isSuitable = suiable;
        if(onSuitable != null)
        {
            onSuitable(this);
        }
    }

}
