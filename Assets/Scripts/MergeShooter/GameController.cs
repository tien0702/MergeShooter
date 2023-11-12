using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DesignPattern;

public class OBSERVER_TOPIC
{
    public static readonly string ON_ENEMY_DIE = "EnemyDie";
    public static readonly string ON_ENEMY_TOUCH_WALL = "EnemyTouchWall";
}

public class GameController : SingletonBehaviour<GameController>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Observer.Instance.AddObserver(OBSERVER_TOPIC.ON_ENEMY_DIE, OnEnemyDie);
        Observer.Instance.AddObserver(OBSERVER_TOPIC.ON_ENEMY_TOUCH_WALL, OnEnemyTouchWall);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Observer.Instance.AddObserver(OBSERVER_TOPIC.ON_ENEMY_DIE, OnEnemyDie);
        Observer.Instance.AddObserver(OBSERVER_TOPIC.ON_ENEMY_TOUCH_WALL, OnEnemyTouchWall);
    }

    void OnEnemyDie(object data)
    {
        if (!(data is EnemyController)) return;
    }

    void OnEnemyTouchWall(object data)
    {
        if (!(data is EnemyController)) return;
    }
}
