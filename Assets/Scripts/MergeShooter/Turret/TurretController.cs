using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.Entity;
using Unity.VisualScripting;
using UnityEngine.UI;
using TT.EntityStat.Base;

public class TurretController : EntityController
{
    public int maxLv;
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
        collider.radius = ObjectMergeController.DistanceMerge;
    }

    private void Start()
    {
        Level = info.Level;
        cooldownAttack = 1000.0f / (float)statCtrl.GetStatByID("ASPD").FinalValue;
    }

    private void Update()
    {
        cooldownAttack -= Time.deltaTime;
        if (cooldownAttack <= 0)
        {
            Shoot();
            cooldownAttack = 1000.0f / (float)statCtrl.GetStatByID("ASPD").FinalValue;
        }
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            animator.Play("idle-" + Level.ToString());
        }
        if (Input.GetMouseButtonDown(0) && Level < maxLv)
        {
            //Level += 1;
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
