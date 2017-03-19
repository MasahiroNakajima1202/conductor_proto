using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class BattleActionAttack : BattleAction
    {
        static readonly int WarmTime = 10;

        static readonly int CoolTime = 60;

        static readonly float AttackRangeDegree = 15.0f;

        static readonly float AttackRangeDistance = 1.5f;

        static readonly int DisableTime = 60;

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

            if (currentTarget == null)
            {
                UpdateDisableState();
            }

            if (!CanHitEnemy())
            {
                // 振り向き動作
                TurnToTarget();
            }
            else
            {
                UpdateReadyToAction();
            }
        }

        public override bool IsFinished()
        {
            return timeCount > WarmTime + CoolTime;
        }

        public override void Reset()
        {
            base.Reset();
            isRunning = false;
            timeCount = 0;
            disableTimeCount = 0;
        }

        public override void Run(Actor actor)
        {
            base.Run(actor);

            isRunning = true;
            timeCount = 0;
        }

        void SearchCurrentTarget()
        {
            currentTarget = null;
            var targets = FindObjectsOfType<Actor>();
            for (int i = 0; i < targets.Length; i++)
            {
                var target = targets[i];
                if (actor.Group == target.Group){ continue; }

                Vector3 toTarget = target.transform.position - actor.transform.position;
                toTarget.y = 0.0f;

                float distance = toTarget.sqrMagnitude;

                if (distance < AttackRangeDistance * AttackRangeDistance)
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
            float threshold = Mathf.Cos(AttackRangeDegree * Mathf.Deg2Rad);

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
            if (disableTimeCount == DisableTime)
            {
                timeCount = WarmTime + CoolTime + 1;
            }

            disableTimeCount++;
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
