using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class StrategyScorerGroupAround : StrategyScorer
    {
        static readonly float ScoreMax = 1.0f;

        [SerializeField]
        Actor.BattleGroup targetGroup;

        [SerializeField]
        float nearLimit;

        [SerializeField]
        float farLimit;

        public override float Score()
        {
            if (nearLimit >= farLimit)
            {
                Debug.LogError(string.Format("Error: {0} nearLimit is larger than farLimit.", gameObject.name));
                return 0.0f;
            }
            
            // find all enemy
            Actor[] actorArray = GameObject.FindObjectsOfType<Actor>();

            float scoreSum = 0.0f;

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
                scoreSum += score;
            }

            scoreSum = Mathf.Clamp01(scoreSum / ScoreMax);

            return scoreSum;
        }
    }
}
