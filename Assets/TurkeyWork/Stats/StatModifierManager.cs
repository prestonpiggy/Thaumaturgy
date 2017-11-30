using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Stats {

    public class StatModifierManager : SingletonBehaviour<StatModifierManager> {

        static List<StatModifierLink> timedBuffs = new List<StatModifierLink> ();

        public void RegisterTimedBuff (StatBase stat, Modifier buff) {
            // Check if the buff is actually permanent and, thus, should not be added.
            if (!buff.IsPermanent)
                timedBuffs.Add (new StatModifierLink (stat, buff));
        }

    }

    internal struct StatModifierLink {
        public readonly StatBase Stat;
        public readonly Modifier Buff;

        public StatModifierLink (StatBase stat, Modifier buff) {
            Stat = stat;
            Buff = buff;
        }

        public void RemoveBuff () {
            Stat.RemoveBuffAndRecalculate (Buff);
        }
    }
}
