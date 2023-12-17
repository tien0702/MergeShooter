using System.Collections;
using System.Collections.Generic;
using TT.DesignPattern;
using TT.Entity;
using TT.EntityStat.Base;
using TT.Process.Common;
using UnityEngine;

public class EnemyController : EntityController, IHit
{
    [SerializeField] protected GameObject deadFx;
    [SerializeField] protected HealthController healthCtrl;

    public HealthController HealthCtrl => healthCtrl;
    protected override void Awake()
    {
        base.Awake();

        info.Level = 1;
        var modelPrefab = Resources.Load<Transform>("Prefabs/Enemies/" + info.Name);
        var model = Instantiate(modelPrefab, transform);
        model.GetComponent<Animator>().Play(info.Name);

        statCtrl = this.GetComponent<StatController>();
        healthCtrl = transform.GetComponentInChildren<HealthController>();
        healthCtrl.OnDie = this.OnDie;

        Level = info.Level;

        Stat hp = statCtrl.GetStatByID(DefineStatID.HP);
        hp.CallbackOnChange((float newVal) =>
        {
            healthCtrl.Health = newVal;
        });
    }

    public void TakeHit(EntityController attacker)
    {
        float damage = attacker.StatCtrl.GetStatByID(DefineStatID.ATK).FinalValue;
        healthCtrl.CurrentValue -= damage;
    }

    protected override void OnLevelUp(int level)
    {
        statCtrl.SetStatInfos(entityTypeVO.GetStatInfos(info.Name, info.Level));
        healthCtrl.Health = statCtrl.GetStatByID(DefineStatID.HP).FinalValue;
    }

    protected void OnDie()
    {
        Observer.Instance.NotifyWithData(OBSERVER_TOPIC.ON_ENEMY_DIE, this);
        Destroy(gameObject);
        Instantiate(deadFx, transform.position, transform.rotation);
    }
}
