using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class FilterView : Filter
    {
        [SerializeField]
        float minDegree;

        [SerializeField]
        float maxDegree;

        Vector3 front;

        float lowerThreshold;

        float upperThreshold;

        protected override void PreProcess()
        {
            if (owner == null) { return; }

            Vector3 tempFront = new Vector3(0.0f, 0.0f, 1.0f);
            Matrix4x4 worldMatrix = owner.transform.localToWorldMatrix;
            tempFront = worldMatrix.MultiplyVector(tempFront);
            tempFront.y = 0.0f;
            tempFront.Normalize();
            front = tempFront;

            lowerThreshold = Mathf.Cos(minDegree * Mathf.Deg2Rad);
            upperThreshold = Mathf.Cos(maxDegree * Mathf.Deg2Rad);
        }

        protected override bool Check(ScoreingPoint point)
        {
            if (owner == null) { return false; }

            Vector3 to = point.position - owner.transform.position;
            to.y = 0.0f;
            to.Normalize();

            float dot = Vector3.Dot(front, to);

            bool ret = upperThreshold <= dot && dot <= lowerThreshold;

            return ret;
        }
    }
}
