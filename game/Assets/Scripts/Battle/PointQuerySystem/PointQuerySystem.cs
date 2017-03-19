using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class PointQuerySystem : MonoBehaviour
    {
        [SerializeField]
        Generator generator;

        [SerializeField]
        Filter[] filters;

        [SerializeField]
        Scorer[] scorers;

        public Vector3 CurrentDestination { get; private set; }


        ScoreingPoint[] currentArray;

        int targetIndex;

        public void UpdateState()
        {
            ShowDebugSphere();

            // generate phase
            if (generator == null){ return; }

            var pointArray = generator.Generate();

            // filtering phase
            if (filters == null) { return; }
            for (int i = 0; i < filters.Length; i++)
            {
                Filter filter = filters[i];
                pointArray = filter.Apply(pointArray);
            }

            // scoring phase
            if (scorers == null) { return; }
            for (int i = 0; i < scorers.Length; i++)
            {
                Scorer scorer = scorers[i];
                pointArray = scorer.Apply(pointArray);
            }

            // decide destination
            float maxScore = 0.0f;
            int indexOfMax = -1;
            for (int i = 0; i < pointArray.Length; i++)
            {
                ScoreingPoint point = pointArray[i];
                if (point.Score > maxScore)
                {
                    indexOfMax = i;
                    maxScore = point.Score;
                }
            }

            if (indexOfMax >= 0)
            {
                CurrentDestination = pointArray[indexOfMax].position;
            }
            else
            {
                CurrentDestination = transform.position;
            }

            targetIndex = indexOfMax;
            currentArray = pointArray;
        }

        public Vector3 GetDestination()
        {
            return CurrentDestination;
        }

        void ShowDebugSphere()
        {
            return;
            if (currentArray == null) { return; }

            for (int i = 0; i < currentArray.Length; i++)
            {
                var point = currentArray[i];
                Color color = new Color(point.Score, 0.0f, 1.0f - point.Score, 1.0f);
                MyDebug.ShowSphere(point.position, color);
            }

            Color targetColor = Color.green;
            MyDebug.ShowSphere(CurrentDestination, targetColor);
        }
    }

    public class ScoreingPoint
    {
        public float Score;

        public Vector3 position;
    }
}
