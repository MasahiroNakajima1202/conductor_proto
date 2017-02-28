using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class Strategy : MonoBehaviour
    {
        BattleAction action;

        PointQuerySystem pqs;

        public PointQuerySystem PQS
        {
            get { return pqs; }
        }

        public void UpdateState() { }

        public BattleAction GetBattleAction()
        {
            return action;
        }

        public Vector3 GetDestination()
        {
            return Vector3.zero;
        }

        public bool ReadyToAction()
        {
            return false;
        }

        private void Awake()
        {
            action = GetComponent<BattleAction>();
            pqs = GetComponent<PointQuerySystem>();
        }
    }
}
