using System;
using System.Collections.Generic;

namespace TT.EntityStat.Base
{
    [System.Serializable]
    public class Stat
    {
        protected StatInfo info;
        protected Dictionary<ModifiType, LinkedList<Bonus>> bonus = new Dictionary<ModifiType, LinkedList<Bonus>>();
        protected float finalValue;

        public StatInfo Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                finalValue = CalculateFinalValue();
            }
        }

        public virtual float FinalValue => finalValue;

        public Stat()
        {
            foreach (ModifiType modifi in Enum.GetValues(typeof(ModifiType)))
            {
                bonus.Add(modifi, new LinkedList<Bonus>());
            }
        }

        public virtual void Update(float dt)
        {
            bool recalculateFinalValue = false;
            foreach (KeyValuePair<ModifiType, LinkedList<Bonus>> bonusType in bonus)
            {
                LinkedList<Bonus> listBonus = bonusType.Value;
                LinkedListNode<Bonus> iterator = listBonus.First;
                while (iterator != null)
                {
                    Bonus bns = iterator.Value;
                    bns.CalculateDuration(dt);
                    if (bns.IsEndBonus)
                    {
                        listBonus.Remove(iterator);
                        recalculateFinalValue = true;
                    }
                    iterator = iterator.Next;
                }
            }

            if (recalculateFinalValue)
                finalValue = CalculateFinalValue();
        }

        public virtual void AddBonus(Bonus bns)
        {
            if (bns == null) return;
            bonus[bns.Info.Modifi].AddLast(bns);
            finalValue = CalculateFinalValue();
        }

        public virtual void RemoveBonus(Bonus bns)
        {
            if (bns == null) return;
            bonus[bns.Info.Modifi].Remove(bns);
            finalValue = CalculateFinalValue();
        }

        protected virtual float CalculateFinalValue()
        {
            if (bonus[ModifiType.Absolute].Count > 0)
                return bonus[ModifiType.Absolute].Last.Value.Info.Value;

            float newFinalValue = 0;
            float baseValue = info.BaseValue;
            float percentValue = 0;
            float numberValue = 0;

            LinkedList<Bonus> bnsBase = bonus[ModifiType.BaseValue];
            LinkedList<Bonus> bnsPercent = bonus[ModifiType.Percent];
            LinkedList<Bonus> bnsNum = bonus[ModifiType.Number];

            foreach (Bonus bns in bnsBase) { baseValue += bns.Info.Value; }
            foreach (Bonus bns in bnsPercent) { percentValue += bns.Info.Value; }
            foreach (Bonus bns in bnsNum) { numberValue += bns.Info.Value; }

            newFinalValue = baseValue + (baseValue * (percentValue / 100f)) + numberValue;

            return newFinalValue;
        }
    }
}
