using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commander.Battle
{
    /// <summary>
    /// プレイヤー同士の当たり判定を記述
    /// </summary>
    public class ActorCollision : MonoBehaviour
    {
        static readonly float Epcilon = 0.005f;

        [SerializeField]
        Actor owner;

        public Actor Owner { get { return owner; } }

        public void OnTriggerStay(Collider other)
        {
            if (owner == null) { return; }
            HitCheckToActor(other);
        }

        void HitCheckToActor(Collider other)
        {
            var otherCollision = other.gameObject.GetComponent<ActorCollision>();
            if (otherCollision == null) { return; }
            if (otherCollision.Owner == null) { return; }

            var otherActor = otherCollision.Owner;

            Vector3 toOther = otherActor.transform.position - owner.transform.position;
            toOther.y = 0.0f;
            toOther.Normalize();

            Vector3 otherPosition = otherActor.transform.position;
            otherPosition += toOther * Epcilon;
            otherActor.transform.position = otherPosition;

            Vector3 actorPosition = owner.transform.position;
            actorPosition -= toOther * Epcilon;
            owner.transform.position = actorPosition;
        }
    }
}
