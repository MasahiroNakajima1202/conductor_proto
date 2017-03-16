using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class DamagePopUp : MonoBehaviour
    {
        [SerializeField]
        float speed;

        [SerializeField]
        float gravity;

        [SerializeField]
        float speedResume;

        [SerializeField]
        int frameLength;

        int displayValue;

        float yVelocity;

        float baseY;

        int timeCount;

        public void Initialize(Vector3 position, int displayValue)
        {
            yVelocity = speed;
            transform.position = position;
            this.displayValue = displayValue;
            baseY = position.y;
            timeCount = 0;
            GetComponent<TextMesh>().text = displayValue.ToString();
        }

        private void Update()
        {
            yVelocity -= gravity;
            yVelocity *= speedResume;
            Vector3 position = transform.position;
            position.y += yVelocity;

            if (position.y <= baseY && yVelocity < 0.0f)
            {
                yVelocity = -yVelocity;
                position.y = baseY + (baseY - position.y);
            }

            transform.position = position;

            timeCount++;

            if (timeCount > frameLength)
            {
                Destroy(gameObject);
            }
        }
        
    }
}