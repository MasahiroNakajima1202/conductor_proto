using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public abstract class FilterView : Filter
    {
        protected override void PreProcess()
        {
        }

        protected override bool Check(ScoreingPoint point)
        {
            return base.Check(point);
        }
    }
}
