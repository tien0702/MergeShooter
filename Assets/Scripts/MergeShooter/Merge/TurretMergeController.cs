using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMergeController : MergeController
{
    TurretController owner;

    protected override void Awake()
    {
        base.Awake();
        owner = transform.GetComponent<TurretController>();
    }

    public override bool CanMerge(MergeController mergeObj)
    {
        TurretController turret = mergeObj.GetComponent<TurretController>();
        return owner.Level == turret.Level;
    }

    public override void Merge(MergeController target, Action callbackOnCompleted)
    {
        owner.Level += 1;
        if(callbackOnCompleted != null) { callbackOnCompleted();}
    }
}
