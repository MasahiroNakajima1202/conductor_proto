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
        [SerializeField]
        Actor owner;

        public Actor Owner { get { return owner; } }

        private void OnCollisionEnter(Collision collision)
        {

        }
    }
}
