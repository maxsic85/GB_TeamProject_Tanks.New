using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class InputHandler : MonoBehaviour,IPlayer
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
            if (ServiceLocatorMonoBehavior.GetService<GameService>().roundData.EndRound)
            {
                targetLockOn.ClearTarget();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                targetLockOn.ChooseTarget();
                Debug.Log("touch"+targetLockOn.name);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                combatHandler.PlayerAttackAction();
            }
            else if (Input.GetMouseButtonDown(2))
            {
                ServiceLocatorMonoBehavior.GetService<GameService>().roundData.EndRound = true;
            }
        }
    }
}
