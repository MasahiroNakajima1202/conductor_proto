using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            UpdateHPGauge();
        }

        void UpdateHPGauge()
        {
            // position
            for (int i = 0; i < hpGauges.Length; i++)
            {
                Vector3 position = actors[i].transform.position;
                Vector3 viewportPosition = worldCamera.WorldToViewportPoint(position);
                Vector3 gaugePosition = canvas.worldCamera.ViewportToWorldPoint(viewportPosition);
                hpGauges[i].transform.position = gaugePosition;
            }

            for (int i = 0; i < hpGauges.Length; i++)
            {
                int life = actors[i].Life;
                int maxLife = actors[i].MaxLife;
                hpGauges[i].GetComponent<Slider>().value = (float)life / (float)maxLife;
            }
        }
    }
}
