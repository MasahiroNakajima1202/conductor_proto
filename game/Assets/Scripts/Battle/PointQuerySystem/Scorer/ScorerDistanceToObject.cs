using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class ScorerDistanceToObject : Scorer
    {
        [SerializeField]
        GameObject target;

        [SerializeField]
        float distance;

        [SerializeField]
        float range;

        protected override void PreProcess()
        {
        }

        protected override float Score(ScoreingPoint point)
        {
            if (owner == null){ return 0.0f; }
            if (range <= 0.0f) { return 0.0f; }

            Vector3 toPoint = point.position - target.transform.position;
            toPoint.y = 0.0f;

            float toPointDistance = toPoint.magnitude;
            float delta = Mathf.Abs(distance - toPointDistance);
            float score = 1.0f - Mathf.Clamp01(delta / range);
            return score;
        }
    }
}
