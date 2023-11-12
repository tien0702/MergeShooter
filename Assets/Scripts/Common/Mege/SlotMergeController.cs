using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMergeController : MonoBehaviour
{
    [SerializeField] protected MergeObjectController objInSlot;
    public MergeObjectController ObjInSlot => objInSlot;

    private void Awake()
    {

    }

    void OnChildAdded()
    {

    }

    public MergeObjectController TakeOut()
    {
        MergeObjectController result = null;
        if (objInSlot != null)
        {
            result = objInSlot;
            objInSlot.transform.SetParent(null);
            objInSlot = null;
        }

        return result;
    }

    public void PutIn(MergeObjectController obj)
    {
        /*if (objInSlot != null)
        {
            objInSlot.transform.SetParent(null);
        }
        else
        {
            this.objInSlot = obj;
            this.objInSlot.transform.SetParent(transform);
        }*/
        this.objInSlot = obj;
        this.objInSlot.transform.SetParent(transform);
    }
}
