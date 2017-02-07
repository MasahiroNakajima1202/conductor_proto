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
