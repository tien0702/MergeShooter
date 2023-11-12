using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    static List<RoadController> _roads = new List<RoadController>();

    public static void AddRoad(RoadController road)
    {
        _roads.Add(road);
    }

    public static RoadController GetRoad(int index)
    {
        if (index < 0 || index >= _roads.Count) return null;
        return _roads[index];
    }

    private void Awake()
    {
        RoadController.AddRoad(this);
    }
}
