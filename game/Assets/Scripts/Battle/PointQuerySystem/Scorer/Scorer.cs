using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public abstract class Scorer : MonoBehaviour
    {
        [SerializeField]
        protected AIActor owner;

        [SerializeField]
        float weight = 1.0f;

        public ScoreingPoint[] Apply(ScoreingPoint[] pointArray)
        {
            PreProcess();

            for (int i = 0; i < pointArray.Length; i++)
            {
                ScoreingPoint point = pointArray[i];
                point.Score += Score(point) * weight;
            }

            return pointArray;
        }

        protected virtual void PreProcess()
        {
        }

        protected virtual float Score(ScoreingPoint point)
        {
            return 0.0f;
        }
    }
}
