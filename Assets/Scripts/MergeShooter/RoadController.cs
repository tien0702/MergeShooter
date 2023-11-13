using System.Collections;
using System.Collections.Generic;
using TT.DesignPattern;
using UnityEngine;

public class RoadController : SingletonBehaviour<RoadController>
{
    List<Transform> _roads = new List<Transform>();
    protected override void Awake()
    {
        base.Awake();
        foreach(Transform road in transform)
        {
            _roads.Add(road);
        }
    }


    public Transform GetRoad(int index)
    {
        if (index < 0 || index >= _roads.Count) return null;
        return _roads[index];
    }
}
