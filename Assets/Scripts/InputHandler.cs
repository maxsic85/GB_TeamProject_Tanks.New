using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS
{
    public class InputHandler : IExecute
    {
        TargetLockOn _targetLockOn;
        CombatHandler _combatHandler;
        public ISavePlayerPosition _saveDataPosition;
        private PlayerStats _player;
        private Button UIdataRound;

        public InputHandler(TargetLockOn targetLockOn,CombatHandler combatHandler, ISavePlayerPosition saveDataPosition,Button uIdataRound)
        {
            _targetLockOn = targetLockOn;
            _combatHandler = combatHandler;
            _saveDataPosition = saveDataPosition;
            _player = GameObject.FindObjectOfType<PlayerStats>();
            UIdataRound = uIdataRound;
            UIdataRound.onClick.AddListener(Save);
         //   UIdataRound.LoadBtn.GetOrAddComponent<Button>().onClick.AddListener(Load);
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
                Save();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Load();
            }
        }

        private void Load()
        {
            _saveDataPosition.Load(_player.CurrentHealth);

            _player.SetHealth(_player.CurrentHealth);
            _player.UpdatePlayerHealthSlider();
            Debug.Log("Save");
        }

        private void Save()
        {
            Debug.Log("Save");
            _saveDataPosition.Save(_player.CurrentHealth);
        }
    }
}
