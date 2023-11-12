using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMergeController : MergeObjectController
{
    [SerializeField] float timeMove = 0.5f;
    [SerializeField] LeanTweenType moveType = LeanTweenType.easeOutQuint;
    [SerializeField] GameObject mergeFx;
    TurretController owner;

    protected override void Awake()
    {
        base.Awake();
        owner = transform.GetComponent<TurretController>();
    }

    public override bool CanMerge(MergeObjectController mergeObj)
    {
        TurretController turret = mergeObj.GetComponent<TurretController>();
        return owner.Level == turret.Level;
    }

    public override void Merge(MergeObjectController target, Action callbackOnCompleted)
    {
        transform.SetParent(target.transform.parent);

        LeanTween.moveLocal(this.gameObject, Vector3.zero, timeMove)
            .setEase(moveType)
            .setOnComplete(() =>
                {
                    Destroy(target.gameObject);
                    owner.Level += 1;
                    var fx = Instantiate(mergeFx, transform);
                    fx.transform.localPosition = Vector3.zero;
                });
    }
}
