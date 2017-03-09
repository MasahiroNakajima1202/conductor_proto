using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class BattleAI : MonoBehaviour
    {
        static readonly float ActionRange = 0.5f;

        [SerializeField]
        AIActor owner;

        [SerializeField]
        Strategy[] strategies;

        [SerializeField]
        GameObject scorer;

        Strategy currentStrategy;

        StrategyScorer[] ScorerArray;

        bool isActing;

        public void LoadFromFile(string filename)
        { }

        public void UpdateState()
        {
            if (owner == null) { return; }

            if (isActing)
            {
                BattleAction action = currentStrategy.GetBattleAction();
                action.UpdateState();

                if (action.IsFinished())
                {
                    action.Reset();
                    isActing = false;
                }
            }
            else
            {
                SelectStrategy();
                UpdatePQS();

                owner.WalkTo(currentStrategy.PQS.CurrentDestination);

                if (ReadyToAction())
                {
                    isActing = true;
                    BattleAction action = currentStrategy.GetBattleAction();
                    action.Run(owner);
                }
            }
        }

        public bool ReadyToAction()
        {
            if (currentStrategy == null) { return false; }
            if (owner == null) { return false; }

            PointQuerySystem pqs = currentStrategy.PQS;
            Vector3 targetPosition = pqs.CurrentDestination;
            Vector3 actorPosition = owner.transform.position;

            Vector3 toTarget = targetPosition - actorPosition;
            toTarget.y = 0.0f;
            
            return Vector3.Dot(toTarget, toTarget) < ActionRange * ActionRange;
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

            // 全strategyに大して評価値を決定
            Dictionary<Strategy, float> scoreMap = scoreMap = strategies.ToDictionary(st => st, st => 0.0f);
            for (int i = 0; i < ScorerArray.Length; i++)
            {
                StrategyScorer scorer = ScorerArray[i];
                float score = scorer.Score();
                StrategyWeight[] weightArray = scorer.StrategyWeightArray;
                for (int j = 0; j < weightArray.Length; j++)
                {
                    StrategyWeight weight = weightArray[j];
                    if (!scoreMap.ContainsKey(weight.Strategy))
                    {
                        continue;
                    }

                    scoreMap[weight.Strategy] += score * weight.Weight;
                }
            }

            // 全strategyに対して最大スコアのものを選出
            float maxScore = 0.0f;
            Strategy strategyOfMax = null;
            for (int i = 0; i < strategies.Length; i++)
            {
                Strategy strategy = strategies[i];
                float score = scoreMap[strategy];
                if (score > maxScore)
                {
                    maxScore = score;
                    strategyOfMax = strategy;
                }
            }

            if (strategyOfMax != null)
            {
                currentStrategy = strategyOfMax;
            }

            for (int i = 0; i < strategies.Length; i++)
            {
                Strategy target = strategies[i];
                target.gameObject.SetActive(false);
            }

            currentStrategy.gameObject.SetActive(true);
        }

        void UpdatePQS()
        {
            PointQuerySystem pqs = currentStrategy.PQS;
            pqs.UpdateState();
        }

        private void Awake()
        {
            ScorerArray = scorer.GetComponents<StrategyScorer>();

            if (strategies.Length > 0)
            {
                currentStrategy = strategies[0];
            }
        }
    }
}
