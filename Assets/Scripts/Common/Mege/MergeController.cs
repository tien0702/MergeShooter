using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MergeController : MonoBehaviour
{
    #region ObjectMerge Manager
    protected static List<MergeController> _merges = new List<MergeController>();
    public static List<MergeController> Merges => _merges;

    public static float DistanceMerge = 0.4f;

    public static void AddMergeObject(MergeController newMergeObject)
    {
        _merges.Add(newMergeObject);
    }

    public static void RemoveMergeObject(MergeController mergeObject)
    {
        _merges.Remove(mergeObject);
    }

    public static MergeController GetMergeNearest(MergeController mergeTarget)
    {
        MergeController result = null;
        float minDistance = 1000;
        foreach (MergeController mergeObj in _merges)
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

    public static MergeController GetMergeable(MergeController mergeTarget)
    {
        MergeController result = null;
        float minDistance = 1000;
        foreach (MergeController mergeObj in _merges)
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

    public static List<MergeController> GetMergeables(MergeController mergeTarget)
    {
        List<MergeController> result = new List<MergeController>();

        foreach (MergeController mergeObj in _merges)
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
        MergeController.AddMergeObject(this);
    }

    protected virtual void OnDestroy()
    {
        MergeController.RemoveMergeObject(this);
    }

    public abstract bool CanMerge(MergeController mergeObj);

    public abstract void Merge(MergeController target, Action callbackOnCompleted);
}
