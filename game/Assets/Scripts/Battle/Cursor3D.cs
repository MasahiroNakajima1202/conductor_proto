using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class Cursor3D : MonoBehaviour
    {
        static readonly float TargetHeightOffset = 1.0f;

        static readonly float BaseRayWidth = 0.1f;

        [SerializeField]
        Camera rayCamera;

        [SerializeField]
        Transform player;

        [SerializeField]
        GameObject image;

        [SerializeField]
        GameObject selectImage;

        [SerializeField]
        LineRenderer line;

        GameObject pointingTarget;

        GameObject selectedTarget;

        bool visible;

        float rayWidth;

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

            FixSelectCursorToTarget();

            UpdateRotation();

            SelectTarget();

            image.SetActive(visible);
            selectImage.SetActive(selectedTarget != null);

            UpdateLine();
        }

        void CalculateHorizontalPosition()
        {
            Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, player.position);

            float distance = 0.0f;
            bool hit = plane.Raycast(ray, out distance);

            rayWidth = BaseRayWidth * Mathf.Sqrt(distance);

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
            bool hit = Physics.SphereCast(ray, rayWidth, out hitInfo, 10.0f, layer);
            Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.red);

            if (hit)
            {
                ActorCollision target = hitInfo.transform.gameObject.GetComponent<ActorCollision>();
                if (target != null
                    && target.Owner != null
                    && target.Owner.Group == Actor.BattleGroup.Party)
                {
                    pointingTarget = target.Owner.gameObject;
                }
            }
        }

        void FixToTarget()
        {
            if (pointingTarget == null) { return; }

            // 誰かの行動を指示しているときは、キャラクターを指さない
            if (selectedTarget != null)
            {
                pointingTarget = null;
                return;
            }

            Vector3 position = pointingTarget.transform.position;
            position.y += TargetHeightOffset;
            transform.position = position;
        }

        void FixSelectCursorToTarget()
        {
            if (selectedTarget == null) { return; }

            Vector3 position = selectedTarget.transform.position;
            position.y += TargetHeightOffset;
            selectImage.transform.position = position;
        }

        void UpdateRotation()
        {
            Quaternion rotation = Quaternion.AngleAxis(3, Vector3.up);
            image.transform.rotation = image.transform.rotation * rotation;
            selectImage.transform.rotation = selectImage.transform.rotation * rotation;
        }

        void SelectTarget()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (pointingTarget != null)
                {
                    selectedTarget = pointingTarget;
                }
                else if (selectedTarget != null)
                {
                    AIActor aiActor = selectedTarget.GetComponent<AIActor>();
                    if (aiActor != null && aiActor.Group == Actor.BattleGroup.Party)
                    {
                        aiActor.SetDefencePosition(transform.position);
                    }

                    selectedTarget = null;
                }
                else
                {
                    selectedTarget = null;
                }
            }

            // 右クリックで解除
            if (Input.GetMouseButtonDown(1))
            {
                if (selectedTarget != null)
                {
                    selectedTarget = null;
                }
            }
        }

        void UpdateLine()
        {
            if (line == null) { return; }

            if (selectedTarget == null)
            {
                line.enabled = false;
                return;
            }

            line.enabled = true;
            Vector3 begin = selectedTarget.transform.position;
            Vector3 end = transform.position;
            line.SetPosition(0, begin);
            line.SetPosition(1, end);
        }

    }
}
