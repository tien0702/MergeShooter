using System.Collections;
using System.Collections.Generic;
using TT.Entity;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected float timeLife = 1f;

    public EntityController owner;

    protected virtual void Update()
    {
        timeLife -= Time.deltaTime;
        if (timeLife < 0)
        {
            DestroyBullet();
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.1f);
        if (hit.transform == null) return;
        IHit[] ihits = hit.transform.GetComponentsInParent<IHit>();
        if (ihits.Length == 0) return;
        foreach (IHit ihit in ihits)
        {
            ihit.TakeHit(owner);
        }

        DestroyBullet();
    }

    void DestroyBullet()
    {
        if (explosionPrefab)
        {
            var fx = Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
