using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class Strategy
    {
        public Strategy()
        {
        }

        public void Update() { }

        public BattleAction GetBattleAction()
        {
            return new BattleAction();
        }

        public Vector3 GetDestination()
        {
            return Vector3.zero;
        }

        public bool ReadyToAction()
        {
            return false;
        }
    }
}
