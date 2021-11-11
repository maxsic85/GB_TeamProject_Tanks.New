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
            if (CombatHandler.Instance._roundData.EndRound)
            {
                targetLockOn.ClearTarget();
            }
           else if (Input.GetMouseButtonDown(0))
            {
                targetLockOn.ChooseTarget();
            }
          else  if (Input.GetKeyDown(KeyCode.Space))
            {               
                combatHandler.PlayerAttackAction();
            }
        }
    }
}
