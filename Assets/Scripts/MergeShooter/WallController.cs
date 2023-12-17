using System.Collections;
using System.Collections.Generic;
using TT.DesignPattern;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) { return; }
        var enemy = collision.transform.GetComponentInParent<EnemyController>();
        enemy.HealthCtrl.CurrentValue -= int.MaxValue;
        Observer.Instance.NotifyWithData(OBSERVER_TOPIC.ON_ENEMY_TOUCH_WALL, enemy);
    }
}
