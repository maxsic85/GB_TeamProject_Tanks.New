using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class InputHandler : MonoBehaviour
    {
        TargetLockOn targetLockOn;
        CombatHandler combatHandler;

        private void Awake()
        {
            targetLockOn = GetComponent<TargetLockOn>();
            combatHandler = FindObjectOfType<CombatHandler>();
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                targetLockOn.ChooseTarget();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {               
                combatHandler.PlayerAttackAction();
            }
        }
    }
}
