using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class BattleActionAttack : BattleAction
    {
        static readonly int WarmTime = 120;

        static readonly float attackRangeDegree = 15.0f;

        static readonly float attackRangeDistance = 2.0f;

        [SerializeField]
        Attack attackPrefab;

        bool isRunning;

        int timeCount;

        Actor currentTarget;

        int disableTimeCount;

        public override void UpdateState()
        {
            if (!isRunning) { return; }
            if (actor == null) { return; }

            SearchCurrentTarget();

            if (!CanHitEnemy())
            {
                // 振り向き動作
                TurnToTarget();
                return;
            }
            else
            {
                UpdateDisableState();
            }

            UpdateReadyToAction();
        }

        public override bool IsFinished()
        {
            return timeCount > WarmTime;
        }

        public override void Reset()
        {
            base.Reset();
            isRunning = false;
            timeCount = 0;
            
        }

        public override void Run(Actor actor)
        {
            base.Run(actor);

            isRunning = true;
            timeCount = 0;
        }

        void SearchCurrentTarget()
        {
            var targets = FindObjectsOfType<Actor>();
            for (int i = 0; i < targets.Length; i++)
            {
                var target = targets[i];
                if (actor.Group == target.Group){ continue; }

                Vector3 toTarget = target.transform.position - actor.transform.position;
                toTarget.y = 0.0f;

                float distance = toTarget.sqrMagnitude;

                if (distance < attackRangeDistance * attackRangeDistance)
                {
                    currentTarget = target;
                    break;
                }
            }
        }

        bool CanHitEnemy()
        {
            if (currentTarget == null) { return false; }

            Vector3 toTarget = currentTarget.transform.position - actor.transform.position;
            toTarget.y = 0.0f;

            float distance = toTarget.magnitude;

            toTarget.Normalize();
            Vector3 front = actor.GetFrontVector();

            float dot = Vector3.Dot(front, toTarget);
            float threshold = Mathf.Cos(attackRangeDegree * Mathf.Deg2Rad);

            return dot > threshold;
        }

        void TurnToTarget()
        {
            if (currentTarget == null) { return; }

            Vector3 toTarget = currentTarget.transform.position - actor.transform.position;
            toTarget.y = 0.0f;
            actor.ChangeRotation(toTarget);
        }

        void UpdateDisableState()
        {
            // kokokara
        }

        void UpdateReadyToAction()
        {
            if (timeCount == WarmTime)
            {
                actor.Attack();
            }

            timeCount++;
        }
    }
}
