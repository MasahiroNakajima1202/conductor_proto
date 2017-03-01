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

        int timeCount;

        protected bool active;

        public virtual void Run(Vector3 position, Vector3 direction)
        {
            GameObject animation = Instantiate(animationPrefab).gameObject;
            animation.transform.SetParent(transform, false);
            transform.position = position;

            active = true;
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
