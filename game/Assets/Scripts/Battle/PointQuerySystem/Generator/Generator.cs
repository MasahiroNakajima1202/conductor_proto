using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public abstract class Generator : MonoBehaviour
    {
        [SerializeField]
        protected GameObject owner;

        public abstract ScoreingPoint[] Generate();
    }
}
