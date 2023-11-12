using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MergeObjectController : MonoBehaviour
{
    #region ObjectMerge Manager
    protected static List<MergeObjectController> _merges = new List<MergeObjectController>();
    public static List<MergeObjectController> Merges => _merges;

    public static float DistanceMerge = 0.5f;

    public static void AddMergeObject(MergeObjectController newMergeObject)
    {
        _merges.Add(newMergeObject);
    }

    public static void RemoveMergeObject(MergeObjectController mergeObject)
    {
        _merges.Remove(mergeObject);
    }

    public static MergeObjectController GetMergeNearest(MergeObjectController mergeTarget)
    {
        MergeObjectController result = null;
        float minDistance = 1000;
        foreach (MergeObjectController mergeObj in _merges)
        {
            if (mergeTarget == mergeObj) continue;
            float distance = Vector3.Distance(mergeObj.transform.position, mergeTarget.transform.position);
            if (distance > DistanceMerge) continue;
            if (distance < minDistance)
            {
                minDistance = distance;
                result = mergeObj;
            }
        }
        return result;
    }

    public static MergeObjectController GetMergeable(MergeObjectController mergeTarget)
    {
        MergeObjectController result = null;
        float minDistance = 1000;
        foreach (MergeObjectController mergeObj in _merges)
        {
            if (mergeTarget == mergeObj) continue;
            float distance = Vector3.Distance(mergeObj.transform.position, mergeTarget.transform.position);
            if (distance > DistanceMerge) continue;
            if (!mergeObj.CanMerge(mergeTarget)) continue;
            if (distance < minDistance)
            {
                minDistance = distance;
                result = mergeObj;
            }
        }
        return result;
    }

    public static List<MergeObjectController> GetMergeables(MergeObjectController mergeTarget)
    {
        List<MergeObjectController> result = new List<MergeObjectController>();

        foreach (MergeObjectController mergeObj in _merges)
        {
            if (mergeTarget == mergeObj) continue;
            if (!mergeObj.CanMerge(mergeTarget)) continue;
            result.Add(mergeObj);
        }

        return result;
    }

    #endregion
    
    protected virtual void Awake()
    {
        MergeObjectController.AddMergeObject(this);
    }

    protected virtual void OnDestroy()
    {
        MergeObjectController.RemoveMergeObject(this);
    }

    public abstract bool CanMerge(MergeObjectController mergeObj);

    public abstract void Merge(MergeObjectController target, Action callbackOnCompleted);
}
