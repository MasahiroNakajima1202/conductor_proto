using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class AI : MonoBehaviour
    {
        public void LoadFromFile(string filename)
        { }

        public void UpdateState()
        {
            SelectStrategy();
            UpdatePQS();
        }

        public bool ReadyToAction()
        {
            // FIXME: 現在のstrategyの指す目標地点と自分の位置が一致していたらtrue
            return false;
        }

        public Vector3 GetDestination()
        {
            return Vector3.zero;
        }

        public BattleAction GetBattleAction()
        {
            return new BattleAction();
        }

        void SelectStrategy()
        { }

        void UpdatePQS()
        { }
    }
}
