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

        public enum State
        {
            Idle,
            Walk,
            Attack,
        }

        [SerializeField]
        protected BattleGroup group;

        [SerializeField]
        protected float WalkLimitDegree = 90.0f;

        [SerializeField]
        protected float OmegaDegree = 1.0f;

        [SerializeField]
        protected float WalkSpeed = 0.01f;

        // FIXME: 設定方法はおいおい
        [SerializeField]
        protected Attack attackPrefab;

        [SerializeField]
        protected Transform attackPosition;

        [SerializeField]
        int attackLatency = 10;

        [SerializeField]
        int attackTimeLength = 30;

        int attackTimeCount;

        protected State state = State.Idle;

        

        public BattleGroup Group
        {
            get { return group; }
        }

        public Transform AttackPosition
        {
            get { return attackPosition; }
        }

        public void Damage() { }

        public bool IsDead() { return false; }

        public void WalkTo()
        {
        }

        public void StartBattleAction()
        {
        }

        public void WalkTo(Vector3 dest)
        {
            if (state == State.Attack) { return; }
            Vector3 front = GetFrontVector();
            Vector3 toDestionation = dest - transform.position;
            toDestionation.y = 0.0f;
            float distance = toDestionation.magnitude;
            toDestionation.Normalize();

            float threshold = Mathf.Cos(WalkLimitDegree * Mathf.Deg2Rad);
            float dot = Vector3.Dot(front, toDestionation);
            if (dot < threshold)
            {
                ChangeRotation(toDestionation);
            }
            else
            {
                ChangeRotation(toDestionation);
                float speed = Mathf.Min(distance, WalkSpeed);

                // FIXME: curveを用いて調整
                speed *= dot * dot;
                transform.position = transform.position + front * speed;
            }
        }

        public virtual void SetDefencePosition(Vector3 position)
        {
        }

        public virtual void ClearDefencePosition(Vector3 position)
        {
        }

        public Vector3 GetFrontVector()
        {
            Vector3 front = new Vector3(0.0f, 0.0f, 1.0f);
            Matrix4x4 worldMatrix = transform.localToWorldMatrix;
            front = worldMatrix.MultiplyVector(front);
            front.y = 0.0f;
            front.Normalize();
            return front;
        }

        public void Attack()
        {
            if (state == State.Attack) { return; }

            attackTimeCount = 0;
            SetState(State.Attack);
        }

        void UpdateBattleAction()
        {
            if (state != State.Attack) { return; }

            if (attackTimeCount >= attackTimeLength)
            {
                SetState(State.Idle);
                return;
            }

            if (attackTimeCount == attackLatency)
            {
                Attack attack = Instantiate(attackPrefab);
                Vector3 position = attackPosition.transform.position;
                Vector3 direction = GetFrontVector();
                attack.Run(position, direction);
            }

            attackTimeCount++;
        }

        void ChangeRotation(Vector3 destFront)
        {
            Quaternion rotation = Quaternion.FromToRotation(GetFrontVector(), destFront);
            float angleDegree = Quaternion.Angle(Quaternion.identity, rotation);
            float ratio = 1.0f;
            if (angleDegree > 0.0f)
            {
                ratio = Mathf.Clamp01(OmegaDegree / angleDegree);
            }
            Quaternion interpolation = Quaternion.Slerp(Quaternion.identity, rotation, ratio);
            transform.rotation = transform.rotation * interpolation;
        }


        // Use this for initialization
        protected virtual void Awake()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            UpdateBattleAction();
        }

        public void SetState(State state)
        {
            if (this.state == state) { return; }

            this.state = state;

            // animation
            Animation animation = GetComponentInChildren<Animation>();
            if (animation == null){ return; }
            Dictionary<State, string> map = new Dictionary<State, string> {
                {State.Idle, "Wait" },
                {State.Walk, "Walk" },
                {State.Attack, "Attack" },
            };

            string name = map[state];
            animation.clip = animation.GetClip(name);
            animation.Play();
        }

        public State GetState()
        {
            return state;
        }
    }
}
