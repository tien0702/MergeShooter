using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMergeController : MonoBehaviour
{
    [SerializeField] protected MergeController objInSlot = null;
    public MergeController ObjInSlot => objInSlot;

    public MergeController TakeOut()
    {
        MergeController result = null;
        if (objInSlot != null)
        {
            result = objInSlot;
            objInSlot.transform.SetParent(null);
            objInSlot = null;
        }
        return result;
    }

    public bool PutIn(MergeController obj)
    {
        if (objInSlot != null)
        {
            return false;
        }
        this.objInSlot = obj;
        this.objInSlot.transform.SetParent(transform);

        return true;
    }
}
