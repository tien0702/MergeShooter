using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TT.Entity
{
    [System.Serializable]
    public class EntityInfo
    {
        public string Name;
        public int Level = 1;

        public EntityInfo(string name, int level = 1)
        {
            this.Name = name;
            this.Level = level;
        }
    }
}
