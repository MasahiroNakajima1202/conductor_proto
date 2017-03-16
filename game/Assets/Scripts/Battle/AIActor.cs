using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commander.Battle.AI;

namespace Commander.Battle
{
    public class AIActor : Actor
    {
        [SerializeField]
        BattleAI ai;

        [SerializeField]
        Transform defencePoint;

        /// <summary>
        /// 使い道ない……？
        /// </summary>
        /// <param name="ai"></param>
        public void AttachAI(BattleAI ai)
        {
            this.ai = ai;
        }

        public override void SetDefencePosition(Vector3 position)
        {
            defencePoint.position = position;
            defencePoint.SetParent(null, true);
        }

        public override void ClearDefencePosition(Vector3 position)
        {
            defencePoint.SetParent(transform, false);
        }

        // Use this for initialization
        protected override void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (state == State.Dead) { return; }

            if (ai != null)
            {
                ai.UpdateState();
            }
        }
    }
}
