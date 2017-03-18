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

        static readonly int ActionWaitLength = 60;

        [SerializeField]
        AIActor owner;

        [SerializeField]
        Strategy[] strategies;

        [SerializeField]
        GameObject scorer;

        Strategy currentStrategy;

        StrategyScorer[] ScorerArray;

        bool isActing;

        int actionWaitCount;

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
                    action = null;
                    isActing = false;
                }
            }
            else if(owner.GetState() == Actor.State.Idle || owner.GetState() == Actor.State.Walk)
            {
                SelectStrategy();
                UpdatePQS();

                bool walked = false;
                Vector3 beforePosition = owner.transform.position;

                owner.WalkTo(currentStrategy.PQS.CurrentDestination);

                Vector3 afterPosition = owner.transform.position;
                Vector3 move = afterPosition - beforePosition;
                walked = move.sqrMagnitude > (0.001f * 0.001f);

                if (walked)
                {
                    owner.SetState(Actor.State.Walk);
                }
                else
                {
                    owner.SetState(Actor.State.Idle);
                }

                UpdateActionWait();

                if (ReadyToAction())
                {
                    isActing = true;
                    BattleAction action = currentStrategy.GetBattleAction();
                    action.Run(owner);
                    actionWaitCount = 0;
                }
            }
        }

        public bool ReadyToAction()
        {
            return actionWaitCount > ActionWaitLength;
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
            Dictionary<Strategy, float> scoreMap = strategies.ToDictionary(st => st, st => 0.0f);
            for (int i = 0; i < ScorerArray.Length; i++)
            {
                StrategyScorer scorer = ScorerArray[i];
                float score = scorer.Score();
                AnimationCurve curve = scorer.Curve;
                score = curve.Evaluate(score);
                StrategyWeight[] weightArray = scorer.StrategyWeightArray;
                for (int j = 0; j < weightArray.Length; j++)
                {
                    StrategyWeight weight = weightArray[j];
                    if (!scoreMap.ContainsKey(weight.Strategy))
                    {
                        continue;
                    }

                    scoreMap[weight.Strategy] += score * weight.Weight;
                    weight.Score = score * weight.Weight;
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

        void UpdateActionWait()
        {
            if (currentStrategy == null) { return; }
            if (owner == null) { return; }

            PointQuerySystem pqs = currentStrategy.PQS;
            Vector3 targetPosition = pqs.CurrentDestination;
            Vector3 actorPosition = owner.transform.position;

            Vector3 toTarget = targetPosition - actorPosition;
            toTarget.y = 0.0f;

            if (Vector3.Dot(toTarget, toTarget) < ActionRange * ActionRange)
            {
                actionWaitCount++;
            }
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
