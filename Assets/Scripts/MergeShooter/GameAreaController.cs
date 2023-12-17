using System.Collections;
using System.Collections.Generic;
using TT.DesignPattern;
using TT.Entity;
using UnityEngine;

public class GameAreaController : SingletonBehaviour<GameAreaController>
{
    Dictionary<string, BoardMergeController> _boards = new Dictionary<string, BoardMergeController>();

    public TurretController prefab;

    [SerializeField] private GameObject roads;
    protected override void Awake()
    {
        base.Awake();

        var boards = this.GetComponentsInChildren<BoardMergeController>();
        for(int i = 0; i < boards.Length; i++)
        {
            _boards.Add(boards[i].BoardType, boards[i]);
        }
    }

    public BoardMergeController GetBoardByType(string name)
    {
        if(_boards.ContainsKey(name))
        {
            return _boards[name];
        }
        return null;
    }

    public void AddTurret()
    {
        var slots = _boards["QueueType"].GetEmptySlots();
        if (slots == null || slots.Length == 0) return;
        var slot = slots[Random.Range(0, slots.Length)];

        var turret = Instantiate(prefab);
        turret.Level = Random.Range(18, 20);
        slot.PutIn(turret.GetComponent<MergeController>());
        turret.transform.localPosition = Vector3.zero;
    }

    public void AddTurret(EntityInfo turretInfo, string boardType)
    {
        if(!_boards.ContainsKey(boardType))
        {
            Debug.Log("Not exists: " +  boardType);
            return;
        }

        if(turretInfo == null)
        {
            Debug.Log("Turret Info = null");
            return;
        }


        var slots = _boards[boardType].GetEmptySlots();
        if (slots == null || slots.Length == 0) return;
        var slot = slots[Random.Range(0, slots.Length)];

        var turret = Instantiate(prefab);
        turret.Level = turretInfo.Level;
        slot.PutIn(turret.GetComponent<MergeController>());
        turret.transform.localPosition = Vector3.zero;
    }
}
