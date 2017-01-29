using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public abstract class GeneratorCircle : Generator
    {
        [SerializeField]
        float radius;

        [SerializeField]
        int radiusDivision;

        [SerializeField]
        float rangeRadian;

        [SerializeField]
        int rangeDivision;

        public override ScoreingPoint[] Generate() {
            Debug.Assert(owner != null, "[PointQuerySystem] GeneratorCircle has no owner.");

            // 中心点は重複しないように
            int pointNum = (rangeDivision + 1) * radiusDivision + 1;
            ScoreingPoint[] array = new ScoreingPoint[pointNum];

            // from inner
            array[0].position = owner.transform.position;

            float radiusStride = radius / (float)radiusDivision;
            float rangeStride = 2.0f * rangeRadian / (float)rangeDivision;
            float pointRadius = radiusStride;
            for (int radiusIndex = 1; radiusIndex <= radiusDivision; radiusIndex++)
            {
                float pointRange = -rangeRadian;
                for (int rangeIndex = 0; rangeIndex <= rangeDivision; rangeIndex++)
                {
                    Vector3 basePosition = owner.transform.position;
                    Vector3 offset = Vector3.zero;
                    Vector3 front = new Vector3(0.0f, 0.0f, -1.0f);

                    offset.x = Mathf.Cos(pointRange) * front.x - Mathf.Sin(pointRange) * front.z;
                    offset.z = Mathf.Sin(pointRange) * front.x + Mathf.Cos(pointRange) * front.z;
                    offset *= pointRadius;

                    // FIXME: fix to ground
                    offset.y = 0.0f;

                    int index = radiusIndex * (rangeDivision + 1) + rangeIndex + 1;
                    ScoreingPoint point = array[index];
                    point.position = basePosition + offset;

                    pointRange += rangeStride;
                }

                pointRadius += radiusStride;
            }

            return array;
        }
    }
}
