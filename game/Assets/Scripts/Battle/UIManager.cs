using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        Canvas canvas;

        [SerializeField]
        Camera worldCamera;

        [SerializeField]
        GameObject HPGaugePrefab;

        Actor[] actors;

        GameObject[] hpGauges;

        private void Awake()
        {
            actors = FindObjectsOfType<Actor>();

            hpGauges = new GameObject[actors.Length];
            for (int i = 0; i < hpGauges.Length; i++)
            {
                hpGauges[i] = Instantiate(HPGaugePrefab, transform, true);
                hpGauges[i].transform.localScale = Vector3.one;
            }
        }

        private void Update()
        {
            for (int i = 0; i < hpGauges.Length; i++)
            {
                Vector3 position = actors[i].transform.position;
                Vector3 viewportPosition = worldCamera.WorldToViewportPoint(position);
                Vector3 gaugePosition = canvas.worldCamera.ViewportToWorldPoint(viewportPosition);
                hpGauges[i].transform.position = gaugePosition;
            }
        }
    }
}
