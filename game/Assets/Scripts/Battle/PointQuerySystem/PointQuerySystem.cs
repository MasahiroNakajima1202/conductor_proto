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

        Vector3 currentDestination;

        public void UpdateState()
        {
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

            for (int i = 0; i < pointArray.Length; i++)
            {
                var point = pointArray[i];
                MyDebug.ShowSphere(point.position, Color.red);
            }
        }

        public Vector3 GetDestination()
        {
            return currentDestination;
        }
    }

    public class ScoreingPoint
    {
        public float Score;

        public Vector3 position;
    }
}
