using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DAL;
using TT.Entity;
using TT.EntityStat.Base;
using SimpleJSON;

namespace TT.Entity.Model
{
    public class EntityDAL : SingleDAL
    {
        public StatInfo[] GetStatInfos(int level)
        {
            if (level <= 0 || level > LengthArray) return null;

            JSONArray array = Array[level - 1].AsArray;
            StatInfo[] statInfos = new StatInfo[array.Count];

            for(int i = 0; i < array.Count; ++i)
            {
                statInfos[i] = JsonUtility.FromJson<StatInfo>(array[i].ToString());
            }

            return statInfos;
        }
    }
}
