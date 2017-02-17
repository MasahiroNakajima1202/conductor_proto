using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class BattleAction : MonoBehaviour
    {
        Actor actor;

        public virtual void UpdateState()
        {
        }

        public virtual bool IsFinished()
        {
            return false;
        }

        public virtual void Reset()
        { }

        public virtual void Run(Actor actor)
        {
            this.actor = actor;
        }
    }
}
