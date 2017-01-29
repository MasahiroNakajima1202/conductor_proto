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

            bool[] activeArray = new bool[pointArray.Length];
            int count = 0;
            for (int i = 0; i < pointArray.Length; i++)
            {
                ScoreingPoint point = pointArray[i];
                activeArray[i] = Check(point);
                if (activeArray[i])
                {
                    count++;
                }
            }

            ScoreingPoint[] newArray = new ScoreingPoint[count];

            int index = 0;
            for (int i = 0; i < pointArray.Length; i++)
            {
                if (!activeArray[i])
                {
                    continue;
                }

                newArray[index] = pointArray[i];
                index++;
            }

            return newArray;
        }

        protected virtual void PreProcess()
        {
        }

        protected virtual bool Check(ScoreingPoint point)
        {
            return true;
        }
    }
}
