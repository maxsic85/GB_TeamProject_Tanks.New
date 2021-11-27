using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class InputHandler : IExecute
    {
        TargetLockOn _targetLockOn;
        CombatHandler _combatHandler;
        public ISavePlayerPosition _saveDataPosition;

        public InputHandler(TargetLockOn targetLockOn,CombatHandler combatHandler)
        {
            _targetLockOn = targetLockOn;
            _combatHandler = combatHandler;
            _saveDataPosition = new SaveDataRep();
        }

        public void Execute(float time)
        {
            if (_targetLockOn == null) return;
            if (ServiceLocator.Resolve<GameStarter>().roundData.EndRound)
            {
                _targetLockOn.ClearTarget();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                _targetLockOn.ChooseTarget();
                Debug.Log("touch" + _targetLockOn.name);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                _combatHandler.PlayerAttackAction();
                Debug.Log("Do Action");
            }
            else if (Input.GetMouseButtonDown(2))
            {
                _saveDataPosition.Save(_combatHandler.PlayerTeam[0].CurrentHealth);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _saveDataPosition.Load(_combatHandler.PlayerTeam[0].CurrentHealth);
            }
        }

    }
}
