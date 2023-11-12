using System.Collections;
using System.Collections.Generic;
using TT.DesignPattern;
using UnityEngine;

public class GameAreaController : SingletonBehaviour<GameAreaController>
{
    [SerializeField] private BoardMergeController battleBoard;
    [SerializeField] private BoardMergeController queueBoard;
    [SerializeField] private GameObject roads;
    


}
