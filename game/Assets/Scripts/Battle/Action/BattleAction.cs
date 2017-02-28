using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class BattleAction : MonoBehaviour
    {
        protected Actor actor;

        public virtual void UpdateState()
        {
        }

        public virtual bool IsFinished()
        {
            return true;
        }

        public virtual void Reset()
        {
            actor = null;
        }

        public virtual void Run(Actor actor)
        {
            this.actor = actor;
        }
    }
}
