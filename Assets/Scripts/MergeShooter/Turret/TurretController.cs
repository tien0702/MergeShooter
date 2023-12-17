using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.Entity;
using UnityEngine.UI;
using TT.EntityStat.Base;
using TT.DesignPattern;
using Unity.VisualScripting;

public class TurretController : EntityController
{
    [SerializeField] protected GameObject model;
    [SerializeField] protected TextMesh lvText;

    [SerializeField] protected GameObject lvUpFx;
    [SerializeField] protected GameObject shootFx;
    [SerializeField] protected BulletController bulletPrefab;

    Transform shootPoints;
    Animator animator;

    public float cooldownAttack = 1.5f;

    protected override void Awake()
    {
        base.Awake();
        statCtrl = GetComponent<StatController>();
        var lvCtrl = Resources.Load("Prefabs/LevelController");

        lvText = Instantiate(lvCtrl, transform).GetComponentInChildren<TextMesh>();

        var collider = this.AddComponent<CircleCollider2D>();
        collider.radius = MergeController.DistanceMerge;
    }

    private void Start()
    {
        Level = info.Level;
        cooldownAttack = 1000.0f / (float)statCtrl.GetStatByID(DefineStatID.ASPD).FinalValue;
    }

    private void Update()
    {
        if(cooldownAttack > 0) cooldownAttack -= Time.deltaTime;
        if (cooldownAttack <= 0 
            && transform.GetComponentInParent<BoardMergeController>() != null
            && transform.GetComponentInParent<BoardMergeController>().BoardType.Equals("BattleType"))
        {
            Shoot();
            cooldownAttack = 1000.0f / (float)statCtrl.GetStatByID(DefineStatID.ASPD).FinalValue;
        }
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            animator.Play("idle-" + Level.ToString());
        }
    }

    protected override void OnLevelUp(int level)
    {
        GameObject lvModel = Resources.Load<GameObject>("Prefabs/TurretModels/" + "Lv" + level.ToString());
        if (lvModel == null)
        {
            Debug.Log("Max lv or can't find prefab");
            return;
        }

        statCtrl.SetStatInfos(entityTypeVO.GetStatInfos(info.Name, level));
        lvText.text = level.ToString();
        Destroy(model);
        cooldownAttack = 0f;
        model = Instantiate(lvModel, transform);
        animator = model.GetComponent<Animator>();
        animator.Play("idle-" + level.ToString());
        shootPoints = model.transform.Find("ShootPoints");
        Instantiate(lvUpFx, transform);
    }

    protected void Shoot()
    {
        if (shootPoints == null) return;
        string animName = "attack-" + info.Level.ToString();
        animator.Play(animName);

        foreach (Transform point in shootPoints)
        {
            Instantiate(shootFx, point);
            var bullet = Instantiate(bulletPrefab, point.position, point.rotation);
            bullet.owner = this;
        }
    }
}
