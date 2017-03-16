using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class AttackCollision : MonoBehaviour {
        [SerializeField]
        Attack owner;

        private void OnTriggerEnter(Collider collider)
        {
            HitCheckToActor(collider);
        }

        void HitCheckToActor(Collider collider)
        {
            var actorCollision = collider.gameObject.GetComponent<ActorCollision>();
            if (actorCollision == null) { return; }

            var actor = actorCollision.Owner;
            if (actor.IsDead){ return; }

            var attack = owner;

            attack.Damage(actor);
        }
    }
}