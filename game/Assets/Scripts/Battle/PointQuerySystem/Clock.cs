using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander.Battle.AI
{
    public class Clock : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            BattleAI.Clock();

            // 暫定的にこいつにシーン切り替え機能
            ChangeScene();
        }

        void ChangeScene()
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                return;
            }

            if (Input.GetKey(KeyCode.Alpha0))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            }
        }
    }
}