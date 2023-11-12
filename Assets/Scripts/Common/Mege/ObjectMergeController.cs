using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ObjectMergeController : MonoBehaviour
{
    #region ObjectMerge Manager
    protected static List<ObjectMergeController> _merges = new List<ObjectMergeController>();
    public static List<ObjectMergeController> Merges => _merges;

    public static float DistanceMerge = 0.5f;

    public static void AddMergeObject(ObjectMergeController newMergeObject)
    {
        _merges.Add(newMergeObject);
    }

    public static void RemoveMergeObject(ObjectMergeController mergeObject)
    {
        _merges.Remove(mergeObject);
    }

    public static ObjectMergeController GetMergeNearest(ObjectMergeController mergeTarget)
    {
        ObjectMergeController result = null;
        float minDistance = 1000;
        foreach (ObjectMergeController mergeObj in _merges)
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

    public static ObjectMergeController GetMergeable(ObjectMergeController mergeTarget)
    {
        ObjectMergeController result = null;
        float minDistance = 1000;
        foreach (ObjectMergeController mergeObj in _merges)
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

    public static List<ObjectMergeController> GetMergeables(ObjectMergeController mergeTarget)
    {
        List<ObjectMergeController> result = new List<ObjectMergeController>();

        foreach (ObjectMergeController mergeObj in _merges)
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
        ObjectMergeController.AddMergeObject(this);
    }

    protected virtual void OnDestroy()
    {
        ObjectMergeController.RemoveMergeObject(this);
    }

    public abstract bool CanMerge(ObjectMergeController mergeObj);

    public abstract void Merge(ObjectMergeController target, Action callbackOnCompleted);
}
