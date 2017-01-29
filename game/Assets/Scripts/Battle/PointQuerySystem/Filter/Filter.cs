using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public abstract class Filter : MonoBehaviour
    {
        public ScoreingPoint[] Apply(ScoreingPoint[] pointArray)
        {
            PreProcess();

            for (int i = 0; i < pointArray.Length; i++)
            {
                ScoreingPoint point = pointArray[i];
                ApplyToPoint(point);
            }

            return pointArray;
        }

        protected virtual void PreProcess()
        {
        }

        protected virtual void ApplyToPoint(ScoreingPoint point)
        {
        }
    }
}
