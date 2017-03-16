using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class ScorerDistanceToGroup : Scorer
    {
        [SerializeField]
        Actor.BattleGroup targetGroup;

        [SerializeField]
        float nearLimit;

        [SerializeField]
        float farLimit;

        protected override void PreProcess()
        {
        }

        protected override void ApplyToPoint(ScoreingPoint point)
        {
            if (nearLimit >= farLimit)
            {
                Debug.LogError(string.Format("Error: {0} nearLimit is larger than farLimit.", gameObject.name));
                return;
            }

            // find all enemy
            Actor[] actorArray = GameObject.FindObjectsOfType<Actor>();

            float maxScore = 0.0f;

            for (int i = 0; i < actorArray.Length; i++)
            {
                Actor target = actorArray[i];

                // 標的以外は無視
                if (target.Group != targetGroup)
                {
                    continue;
                }

                // 距離算出
                Vector3 toTarget = target.transform.position - transform.position;
                toTarget.y = 0.0f;
                float distance = toTarget.magnitude;

                float score = 1.0f - Mathf.Clamp01((distance - nearLimit) / (farLimit - nearLimit));
                maxScore = Mathf.Max(score, maxScore);
            }

            point.Score += maxScore;
        }
    }
}
