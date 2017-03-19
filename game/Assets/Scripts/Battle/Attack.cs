using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class Attack : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem animationPrefab;

        [SerializeField]
        int timeLimit;

        [SerializeField]
        int attackPower;

        [SerializeField]
        int damageTimes;

        int timeCount;

        int damageCount;

        protected bool active;

        Vector3 direction;

        Actor owner;

        public Actor Owner { get { return owner; } }

        public Vector3 Direction { get { return direction; } }

        public virtual void Run(Vector3 position, Vector3 direction, Actor owner)
        {
            if (animationPrefab != null)
            {
                GameObject animation = Instantiate(animationPrefab).gameObject;
                animation.transform.SetParent(transform, false);
            }
            transform.position = position;
            this.owner = owner;
            this.direction = direction;

            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, direction);
            transform.rotation = rotation;

            active = true;
        }

        public virtual void Damage(Actor target)
        {
            if (owner.Group == target.Group) { return; }

            if (damageCount >= damageTimes)
            {
                Destroy(gameObject);
                return;
            }

            int damage = attackPower + owner.AttackPower;

            float dot = Vector3.Dot(direction, target.GetFrontVector());
            float ratio = Mathf.Clamp01((dot + 1.0f) * 0.6f);
            float damageRatio = Mathf.Lerp(0.5f, 1.0f, ratio);

            int directionalDamage = (int)((float)damage * damageRatio);

            target.DamageWithDiffence(directionalDamage);

            damageCount++;
        }

        // Use this for initialization
        protected virtual void Awake()
        {
            
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!active){ return; }

            if (timeLimit < timeCount)
            {
                Destroy(gameObject);
            }

            timeCount++;
        }
    }
}
