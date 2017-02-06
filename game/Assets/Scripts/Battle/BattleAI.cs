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

        [SerializeField, Space(10)]
        StrategySet[] strategySelection;

        StrategySet currentStrategySet;

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
            if (strategySelection == null)
            {
                return;
            }

            if (strategySelection.Length == 0)
            {
                return;
            }

            float maxScore = 0.0f;
            currentStrategySet = strategySelection[0];
            for (int i = 0; i < strategySelection.Length; i++)
            {
                StrategySet target = strategySelection[i];
                float scoreSum = 0.0f;
                for (int j = 0; j < target.Scorer.Length; j++)
                {
                    WeightedStrategyScorer weightedScorer = target.Scorer[j];
                    float score = weightedScorer.Scorer.Score();
                    scoreSum += score * weightedScorer.Weight;
                }

                if (maxScore < scoreSum)
                {
                    currentStrategySet = target;
                    maxScore = scoreSum;
                }
            }

            for (int i = 0; i < strategySelection.Length; i++)
            {
                StrategySet target = strategySelection[i];
                target.Strategy.gameObject.SetActive(false);
            }

            currentStrategySet.Strategy.gameObject.SetActive(true);
        }

        void UpdatePQS()
        {
            PointQuerySystem pqs = currentStrategySet.Strategy.PQS;

            pqs.UpdateState();
        }
    }

    [Serializable]
    public struct StrategySet
    {
        public Strategy Strategy;
        public WeightedStrategyScorer[] Scorer;
    }

    [Serializable]
    public struct WeightedStrategyScorer
    {
        public StrategyScorer Scorer;
        [Range(-10.0f, 10.0f)]
        public float Weight;
    }
}
