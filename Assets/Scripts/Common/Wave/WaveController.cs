using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.Entity;
using TT.DesignPattern;

[System.Serializable]
public class EnemyWaveInfo
{
    public EntityInfo EnemyInfo;
    public int RoadIndex;
    public float DelaySpawn;
}

[System.Serializable]
public class WaveInfo
{
    public EnemyWaveInfo[] Enemies;
}

public class WaveController : SingletonBehaviour<WaveController>
{
    public WaveInfo[] waveInfos;

    protected override void Awake()
    {
        WaveDAL waveDAL = new WaveDAL();
        waveInfos = waveDAL.GetWaveInfos();
    }

    private void Start()
    {
        Observer.Instance.AddObserver(OBSERVER_TOPIC.ON_ENEMY_DIE, OnEnemyDie);
        Observer.Instance.AddObserver(OBSERVER_TOPIC.ON_ENEMY_TOUCH_WALL, OnEnemyTouchWall);
    }

    protected override void OnDestroy()
    {
        Observer.Instance.RemoveObserver(OBSERVER_TOPIC.ON_ENEMY_DIE, OnEnemyDie);
        Observer.Instance.RemoveObserver(OBSERVER_TOPIC.ON_ENEMY_TOUCH_WALL, OnEnemyTouchWall);
    }

    public void StartWaveAt(int index)
    {
        if (index < 0 || index >= waveInfos.Length)
        {

        }
        else
        {
            WaveInfo wave = waveInfos[index];
            StartCoroutine(GenerateWave(wave));
        }
    }

    IEnumerator GenerateWave(WaveInfo wave)
    {
        var enemyPrefab = Resources.Load<EnemyController>("Prefabs/EnemyController");
        EnemyWaveInfo[] enemies = wave.Enemies;
        for (int i = 0; i < enemies.Length; ++i)
        {
            Transform road = RoadController.Instance.GetRoad(enemies[i].RoadIndex);
            var enemy = Instantiate(enemyPrefab, road);
            enemy.Info = enemies[i].EnemyInfo;
            enemy.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(enemies[i].DelaySpawn);
        }
        yield return null;
    }

    void OnEnemyDie(object data)
    {

    }

    void OnEnemyTouchWall(object data)
    {

    }
}
