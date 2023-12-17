using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.Entity;
using TT.DesignPattern;
using TMPro;

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
    #region Observer
    public enum WaveEventType { OnStartWave, OnCleared }
    private ObserverEvents<WaveEventType, int> waveEvent = new ObserverEvents<WaveEventType, int>();
    public ObserverEvents<WaveEventType, int> WaveEvent => waveEvent;

    #endregion

    [SerializeField] private TextMeshProUGUI waveTxt;

    WaveInfo[] waveInfos;
    int currentWave;
    public int MaxWaves => waveInfos.Length;

    List<EnemyController> enemies = new List<EnemyController>();
    int countEnemies;

    protected override void Awake()
    {
        WaveDAL waveDAL = new WaveDAL();
        waveInfos = waveDAL.GetWaveInfos();
    }

    private void Start()
    {
        Observer.Instance.AddObserver(OBSERVER_TOPIC.ON_ENEMY_DIE, OnEnemyDie);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Observer.Instance.RemoveObserver(OBSERVER_TOPIC.ON_ENEMY_DIE, OnEnemyDie);
    }

    public void StartWaveAt(int index)
    {
        if (index < 0 || index >= waveInfos.Length)
        {
            waveEvent.Notify(WaveEventType.OnCleared, waveInfos.Length);
        }
        else
        {
            currentWave = index;
            waveTxt.text = "wave " + (index + 1).ToString();
            WaveInfo wave = waveInfos[index];
            countEnemies = wave.Enemies.Length;
            StartCoroutine(GenerateWave(wave));
        }
    }

    IEnumerator GenerateWave(WaveInfo wave)
    {
        var enemyPrefab = Resources.Load<EnemyController>("Prefabs/EnemyController");
        EnemyWaveInfo[] enemyInfos = wave.Enemies;
        for (int i = 0; i < enemyInfos.Length; ++i)
        {
            Transform road = RoadController.Instance.GetRoad(enemyInfos[i].RoadIndex);
            var enemy = Instantiate(enemyPrefab, road);
            enemies.Add(enemy);
            enemy.Info = enemyInfos[i].EnemyInfo;
            enemy.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(enemyInfos[i].DelaySpawn);
        }
        yield return null;
    }

    void OnEnemyDie(object data)
    {
        --countEnemies;
        if (countEnemies == 0)
        {
            OnCleared();
        }
    }

    void OnCleared()
    {
        waveEvent.Notify(WaveEventType.OnCleared, currentWave);
    }
}
