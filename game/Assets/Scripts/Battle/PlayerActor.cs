using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class PlayerActor : Actor
    {
        // Use this for initialization
        protected override void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            RotateByInput();
            WalkByInput();
            AttackByInput();
        }

        void RotateByInput()
        {
            if (state == State.Attack) { return; }

            float angle = 0.0f;
            if (Input.GetKey(KeyCode.D))
            {
                angle += OmegaDegree;
            }

            if (Input.GetKey(KeyCode.A))
            {
                angle -= OmegaDegree;
            }


            Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0.0f, 1.0f, 0.0f));

            transform.rotation = transform.rotation * rotation;
        }

        void WalkByInput()
        {
            if (state == State.Attack) { return; }

            Vector3 front = GetFrontVector();
            float speed = 0.0f;
            bool walked = false;
            if (Input.GetKey(KeyCode.W))
            {
                speed += WalkSpeed;
                walked = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                speed -= WalkSpeed;
                walked = true;
            }

            if (walked)
            {
                SetState(State.Walk);
            }
            else
            {
                SetState(State.Idle);
            }

            transform.position = transform.position + front * speed;
        }

        void AttackByInput()
        {
            if (attackPosition == null || attackPrefab == null){ return; }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
    }
}
