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

    [Serializable]
    public struct StrategySet
    {
        public Strategy Strategy;
        public WeihtedStrategyScorer[] Scorer;
    }

    [Serializable]
    public struct WeihtedStrategyScorer
    {
        public StrategyScorer Scorer;
        [Range(0.0f, 1.0f)]
        public float Weight;
        [Range(0.0f, 1.0f)]
        public float ScoreView;
    }
}
