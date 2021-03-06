﻿using System.Collections;
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

        int timeCount;

        protected bool active;

        Actor owner;

        public Actor Owner { get { return owner; } }

        public virtual void Run(Vector3 position, Vector3 direction, Actor owner)
        {
            if (animationPrefab != null)
            {
                GameObject animation = Instantiate(animationPrefab).gameObject;
                animation.transform.SetParent(transform, false);
            }
            transform.position = position;
            this.owner = owner;

            active = true;
        }

        public virtual void Damage(Actor target)
        {
            if (owner.Group == target.Group) { return; }

            // FIXME: 計算式きちんと
            target.Damage(5);
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
