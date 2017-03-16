using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class StrategyScorer : MonoBehaviour
    {
        [SerializeField]
        StrategyWeight[] strategyWeightArray;

        [SerializeField]
        AnimationCurve curve;

        public StrategyWeight[] StrategyWeightArray
        {
            get { return strategyWeightArray; }
        }

        public AnimationCurve Curve { get { return curve; } }

        public virtual float Score()
        {
            return 0.0f;
        }
    }

    [Serializable]
    public class StrategyWeight
    {
        [SerializeField]
        float weight;

        [SerializeField]
        Strategy strategy;

        /// <summary>
        /// 監視用
        /// </summary>
        [SerializeField]
        float score;

        public float Score {
            get { return score; }
            set { score = value; }
        }

        public float Weight
        {
            get { return weight; }
        }

        public Strategy Strategy
        {
            get { return strategy; }
        }
    }
}
