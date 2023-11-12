using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class ForwardMovement : MonoBehaviour, ITypeMovement
{
    public Vector3 GetDirectionMove()
    {
        return transform.up;
    }
}