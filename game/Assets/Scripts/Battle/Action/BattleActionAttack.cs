using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class BattleActionAttack : BattleAction
    {
        static readonly int WarmTime = 60;

        [SerializeField]
        Attack attackPrefab;

        bool isRunning;

        int timeCount;

        public override void UpdateState()
        {
            if (!isRunning) { return; }
            if (actor == null) { return; }

            if (timeCount == WarmTime)
            {
                actor.Attack();
            }

            timeCount++;
        }

        public override bool IsFinished()
        {
            return timeCount > WarmTime;
        }

        public override void Reset()
        {
            base.Reset();
            isRunning = false;
            timeCount = 0;
            
        }

        public override void Run(Actor actor)
        {
            base.Run(actor);

            isRunning = true;
            timeCount = 0;
        }
    }
}
