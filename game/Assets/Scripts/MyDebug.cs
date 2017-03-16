using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander
{
    public class MyDebug : MonoBehaviour
    {
        static readonly int sphereCount = 256;

        [SerializeField]
        GameObject spherePrefab;

        static GameObject[] sphereArray;

        static int currentFrameIndex;

        public static void ShowSphere(Vector3 position)
        {
            ShowSphere(position, Color.white);
        }

        public static void ShowSphere(Vector3 position, Color color)
        {
            if (currentFrameIndex >= sphereCount)
            {
                Debug.Log(string.Format("Too many debug spheres. {0} count", currentFrameIndex));
                currentFrameIndex++;
                return;
            }

            GameObject sphere = sphereArray[currentFrameIndex];
            sphere.SetActive(true);

            sphere.transform.position = position;

            Renderer renderer = sphere.GetComponent<Renderer>();
            renderer.material.color = color;

            currentFrameIndex++;
        }

        // Use this for initialization
        void Awake()
        {
            sphereArray = new GameObject[sphereCount];

            for (int i = 0; i < sphereCount; i++)
            {
                GameObject sphere = Instantiate(spherePrefab);
                sphere.SetActive(false);
                sphere.transform.SetParent(transform);
                sphereArray[i] = sphere;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // reset all
            for (int i = 0; i < sphereCount; i++)
            {
                GameObject sphere = sphereArray[i];
                sphere.SetActive(false);
            }

            currentFrameIndex = 0;
        }
    }
}
