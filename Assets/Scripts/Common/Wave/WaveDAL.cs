using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DAL;

public class WaveDAL : SingleDAL
{
    public WaveDAL()
    {
        this.LoadData("waves");
    }

    public WaveInfo[] GetWaveInfos()
    {
        return GetDatas<WaveInfo>();
    }
}
