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

        protected override void ApplyToPoint(ScoreingPoint point)
        {
            if (owner == null){ return; }
            if (range <= 0.0f) { return; }

            Vector3 toPoint = point.position - target.transform.position;
            toPoint.y = 0.0f;

            float toPointDistance = toPoint.magnitude;
            float delta = Mathf.Abs(distance - toPointDistance);
            float score = 1.0f - Mathf.Clamp01(delta / range);
            point.Score += score;

            Debug.Log(score.ToString() + "めう");
        }
    }
}
