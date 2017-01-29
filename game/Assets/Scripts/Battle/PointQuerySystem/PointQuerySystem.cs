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
        { }

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
