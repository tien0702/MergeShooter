using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DesignPattern;

public class OBSERVER_TOPIC
{
    public static readonly string ON_ENEMY_DIE = "EnemyDie";
    public static readonly string ON_ENEMY_TOUCH_WALL = "EnemyTouchWall";
    public static readonly string ON_FINAL = "Final";
}

public class GameController : SingletonBehaviour<GameController>
{
    [SerializeField] Transform waveClearedPanel;
    [SerializeField] Transform waveFinalPanel;
    [SerializeField] Transform waveFailedPanel;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        WaveController.Instance.StartWaveAt(UserData.Instance.WaveIndex);
        WaveController.Instance.WaveEvent
            .RegisterEvent(WaveController.WaveEventType.OnCleared, OnWaveCleared);
    }

    void OnWaveCleared(int waveIndex)
    {
        if (UserData.Instance.WaveIndex >= WaveController.Instance.MaxWaves - 1)
        {
            Observer.Instance.Notify(OBSERVER_TOPIC.ON_FINAL);
            waveFinalPanel.gameObject.SetActive(true);
        }
        else
        {
            waveClearedPanel.gameObject.SetActive(true);
            UserData.Instance.WaveIndex += 1;
        }
    }

    public void PlayWave()
    {
        WaveController.Instance.StartWaveAt(UserData.Instance.WaveIndex);
    }
}
