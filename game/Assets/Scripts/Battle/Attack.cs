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

        // Use this for initialization
        protected virtual void Awake()
        {
            GameObject animation = Instantiate(animationPrefab).gameObject;
            animation.transform.SetParent(transform, false);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (timeLimit < timeCount)
            {
                Destroy(gameObject);
            }

            timeCount++;
        }
    }
}
