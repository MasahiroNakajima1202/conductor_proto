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

        }

        // Update is called once per frame
        protected override void Update()
        {
            RotateByInput();
            WalkByInput();
            AttackByInput();
        }

        void RotateByInput()
        {
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

            if (Input.GetKeyDown(KeyCode.Z)) {
                SetState(State.Idle);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                SetState(State.Walk);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                SetState(State.Attack);
            }
        }

        void WalkByInput()
        {
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
                Attack attack = Instantiate(attackPrefab);
                Vector3 position = attackPosition.transform.position;
                Vector3 direction = GetFrontVector();
                attack.Run(position, direction);
            }
        }
    }
}
