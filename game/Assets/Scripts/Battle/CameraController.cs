using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commander.Battle
{
    public class CameraController : MonoBehaviour
    {
        static readonly float Omega = 1.0f;
        [SerializeField]
        float angleRangeDegree;

        float currentDegree;

        float destDegree;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                destDegree = Mathf.Min(destDegree + Omega, angleRangeDegree);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                destDegree = Mathf.Max(destDegree - Omega, -angleRangeDegree);
                destDegree -= Omega;
            }
            else
            {
                destDegree *= 0.95f;
            }

            currentDegree = Mathf.Lerp(currentDegree, destDegree, 0.95f);

            Quaternion rotation = Quaternion.AngleAxis(currentDegree, Vector3.up);
            transform.localRotation = rotation;
        }
    }
}
