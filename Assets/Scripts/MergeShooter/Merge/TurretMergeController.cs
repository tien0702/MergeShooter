using System;

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

        int maxLv = EnemyController.GetEntityTypeVO(owner.Type).GetBaseSingleVO(owner.EntityName).LengthArray;

        return owner.Level == turret.Level && owner.Level < maxLv;
    }

    public override void Merge(MergeController target, Action callbackOnCompleted)
    {
        owner.Level += 1;
        if(callbackOnCompleted != null) { callbackOnCompleted();}
    }
}
