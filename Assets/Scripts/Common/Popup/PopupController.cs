using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class PopupController : TTMonoBehaviour
{
    public enum TransformType { MoveType, ScaleType, RotateType }

    public enum ActionType { Absolute, Relative }

    public TransformType type;
    public ActionType actionType;

    Vector3 originValue;

    private void OnEnable()
    {
        
    }
}
