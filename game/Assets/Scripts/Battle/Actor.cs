using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class Actor : MonoBehaviour
    {
        public enum BattleGroup
        {
            Party,
            Troop,
            Neutral,
        }

        [SerializeField]
        BattleGroup group;

        public void Damage()
        { }

        public bool IsDead()
        { return false; }

        public void WalkTo()
        {
        }

        public void StartBattleAction()
        { }

        void UpdateBattleAction()
        { }

        // Use this for initialization
        protected virtual void Awake()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }
    }
}
