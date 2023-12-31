using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT.DAL;
using TT.Entity;
using TT.EntityStat.Base;
using System.Linq;

namespace TT.Entity.Model
{
    public class EntityTypeDAL : MultiDAL
    {
        public EntityTypeDAL(string type) 
        {
            this.LoadData<EntityDAL>(type);
        }

        public EntityDAL[] GetEntityVOs()
        {
            return System.Array.ConvertAll(dic.Values.ToArray(), entityVO => (EntityDAL)entityVO);
        }

        public StatInfo[] GetStatInfos(string name, int level)
        {
            if (!dic.ContainsKey(name))
            {
                Debug.Log(string.Format("Entity name: {0} doesn't exists!", name));
                return null;
            }

            return (dic[name] as EntityDAL).GetStatInfos(level);
        }
    }
}
