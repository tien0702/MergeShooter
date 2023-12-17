using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TT.DAL;

public class RewardDAL : SingleDAL
{
    public RewardDAL()
    {
        this.LoadData("rewards");
    }

    public Reward[] GetRewards()
    {
        return GetDatas<Reward>();
    }
}
