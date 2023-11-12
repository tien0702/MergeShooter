using System.Collections;
using System.Collections.Generic;
using TT.Entity;
using TT.EntityStat.Base;
using TT.Process.Common;
using UnityEngine;

public class EnemyController : EntityController, IHit
{
    [SerializeField] protected GameObject deadFx;
    [SerializeField] protected HealthController healthCtrl;
    protected override void Awake()
    {
        base.Awake();

        info.Level = 1;
        info.Name = "monster-1";
        statCtrl = this.GetComponent<StatController>();
        healthCtrl = transform.GetComponentInChildren<HealthController>();
        healthCtrl.OnDie = this.OnDie;

        Level = info.Level;

    }

    public void TakeHit(EntityController attacker)
    {
        float damage = attacker.StatCtrl.GetStatByID("ATK").FinalValue;
        healthCtrl.CurrentValue -= damage;
    }

    protected override void OnLevelUp(int level)
    {
        statCtrl.SetStatInfos(entityTypeVO.GetStatInfos(info.Name, info.Level));
        healthCtrl.Health = statCtrl.GetStatByID("HP").FinalValue;
    }

    protected void OnDie()
    {
        Destroy(gameObject);
        Instantiate(deadFx, transform.position, transform.rotation);
    }
}
