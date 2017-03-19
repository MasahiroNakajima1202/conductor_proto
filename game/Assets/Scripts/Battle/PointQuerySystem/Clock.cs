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
        }
    }
}