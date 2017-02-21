using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class Cursor3D : MonoBehaviour
    {
        static readonly float TargetHeightOffset = 0.25f;
        [SerializeField]
        Camera rayCamera;

        [SerializeField]
        Transform player;

        [SerializeField]
        GameObject image;

        GameObject pointingTarget;

        bool visible;

        // Use this for initialization
        protected virtual void Awake()
        {
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            visible = false;
            pointingTarget = null;

            CalculateHorizontalPosition();
            SearchPointingTarget();

            if (pointingTarget == null)
            {
                FixToFieldHeight();
            }
            else
            {
                FixToTarget();
            }

            UpdateRotation();

            image.SetActive(visible);
        }

        void CalculateHorizontalPosition()
        {
            Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, player.position);

            float distance = 0.0f;
            bool hit = plane.Raycast(ray, out distance);

            if (hit)
            {
                transform.position = ray.origin + ray.direction * distance;
                visible = true;
            }

            Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.red);
        }

        void FixToFieldHeight()
        {
        }

        void SearchPointingTarget()
        {
            if (!visible) { return; }

            Vector3 origin = transform.position;
            origin.y += 10.0f;
            Ray ray = new Ray(origin, -Vector3.up);
            RaycastHit hitInfo = new RaycastHit();
            int layerIndex = LayerMask.NameToLayer("Actor");
            int layer = 1 << (layerIndex);
            bool hit = Physics.Raycast(ray, out hitInfo, 10.0f, layer);
            Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.red);

            if (hit)
            {
                pointingTarget = hitInfo.transform.gameObject;
            }
        }

        void FixToTarget()
        {
            if (pointingTarget == null) { return; }

            Vector3 position = pointingTarget.transform.position;
            position.y += pointingTarget.transform.position.y + TargetHeightOffset;
            transform.position = position;
        }

        void UpdateRotation()
        {
            Quaternion rotation = Quaternion.AngleAxis(3, Vector3.up);
            image.transform.rotation = image.transform.rotation * rotation;
        }

    }
}
