using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMergeController : MonoBehaviour
{
    ObjectMergeController objInSlot;
    public ObjectMergeController ObjInSlot => objInSlot;

    public ObjectMergeController TakeOut()
    {
        ObjectMergeController result = null;
        if (objInSlot != null)
        {
            result = objInSlot;
            objInSlot.transform.SetParent(null);
            objInSlot = null;
        }

        return result;
    }

    public ObjectMergeController PutIn(ObjectMergeController obj)
    {
        if (objInSlot != null)
        {
            objInSlot.transform.SetParent(null);
            return objInSlot;
        }
        this.objInSlot = obj;
        this.objInSlot.transform.SetParent(transform);
        return null;
    }
}
