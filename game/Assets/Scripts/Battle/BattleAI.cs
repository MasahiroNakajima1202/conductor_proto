using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class BattleAI : MonoBehaviour
    {
        [SerializeField]
        Strategy[] strategies;

        [SerializeField]
        GameObject scorer;

        Strategy currentStrategy;

        StrategyScorer[] ScorerArray;

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
        {
            if (ScorerArray == null)
            {
                return;
            }

            if (ScorerArray.Length == 0)
            {
                return;
            }

            float maxScore = 0.0f;
            // ScorerArrayを全部評価して最大のものをサーチ dictionaryでstrategyごとの評価値を管理するのがよさげ ここから

            for (int i = 0; i < strategies.Length; i++)
            {
                Strategy target = strategies[i];
                target.gameObject.SetActive(false);
            }

            // currentStrategyWeight.Strategy.gameObject.SetActive(true);
        }

        void UpdatePQS()
        {
            PointQuerySystem pqs = currentStrategy.PQS;

            pqs.UpdateState();
        }

        private void Awake()
        {
            ScorerArray = scorer.GetComponents<StrategyScorer>();
        }
    }
}
