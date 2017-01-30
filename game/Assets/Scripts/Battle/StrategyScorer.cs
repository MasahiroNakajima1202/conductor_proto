using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class StrategyScorer : MonoBehaviour
    {
        public virtual float Score()
        {
            return 0.0f;
        }
    }
}
