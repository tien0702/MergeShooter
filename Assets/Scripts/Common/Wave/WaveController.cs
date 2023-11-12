using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.Entity;
using TT.DesignPattern;

public class WaveInfo
{
    public EntityInfo[] EntityInfos;
}

public class WaveController : SingletonBehaviour<WaveController>
{
    public WaveInfo waveInfo;
}
