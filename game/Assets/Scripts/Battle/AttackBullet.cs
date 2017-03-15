using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class AttackBullet : Attack
    {
        [SerializeField]
        float speed;

        Vector3 velocity;

        public override void Run(Vector3 position, Vector3 direction, Actor owner)
        {
            base.Run(position, direction, owner);

            velocity = direction * speed;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (!active) { return; }

            Vector3 position = transform.position;
            position += velocity;
            transform.position = position;

            base.Update();
        }
    }
}
