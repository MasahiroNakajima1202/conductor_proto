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
        }

        void WalkByInput()
        {
            Vector3 front = GetFrontVector();
            float speed = 0.0f;
            if (Input.GetKey(KeyCode.W))
            {
                speed += WalkSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                speed -= WalkSpeed;
            }

            Debug.Log(speed);

            transform.position = transform.position + front * speed;
        }
    }
}
