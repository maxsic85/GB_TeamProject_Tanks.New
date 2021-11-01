using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AS
{
    public class CombatHandler : MonoBehaviour
    {
        private List<CharacterStats> _combatants;
        private PlayerStats[] _playerTeam;
        private EnemyStats[] _enemyTeam;
        private List<CharacterStats> _remainingEnemies;
        private List<CharacterStats> _remainingAllies;

        private CharacterStats _currentActiveUnit;
        [HideInInspector] public CharacterStats _currentAIUnitTarget;

        private bool _waitingForPlayerAction;

        private void Awake()
        {
            _combatants = new List<CharacterStats>();
            _remainingAllies = new List<CharacterStats>();
            _remainingEnemies = new List<CharacterStats>();
            _playerTeam = FindObjectsOfType<PlayerStats>();
            _enemyTeam = FindObjectsOfType<EnemyStats>();
        }

        private void Start()
        {
            InitPlayers();
            InitEnemies();
            Battle();
        }

        public void Battle()
        {
            if (_remainingAllies.Count == 0)
            {
                Debug.Log("Defeat");
            }

            if (_remainingEnemies.Count == 0)
            {
                Debug.Log("Victory");
            }
            else
            {
                _currentActiveUnit = _combatants[0];
                _combatants.Remove(_currentActiveUnit);
                _combatants.Add(_currentActiveUnit);
                if (!_currentActiveUnit.IsDead)
                {
                    CheckingTarget();
                }
                else
                {
                    Debug.Log("Dead -> skip");
                    _combatants.Remove(_currentActiveUnit);
                    if (_currentActiveUnit.CompareTag("Enemy"))
                    {
                        _remainingEnemies.Remove(_currentActiveUnit);
                    }
                    else if (_currentActiveUnit.CompareTag("Player"))
                    {
                        _remainingAllies.Remove(_currentActiveUnit);
                    }

                    Battle();
                }
            }
        }

        public void CheckingTarget()
        {
            if (_currentActiveUnit.CompareTag("Enemy"))
            {
                if (_remainingAllies.Count != 0)
                {
                    int index = Random.Range(0, _remainingAllies.Count);
                    _currentAIUnitTarget = _remainingAllies[index];
                    AIAttackAction();
                }
            }
            else if (_currentActiveUnit.CompareTag("Player"))
            {
                _waitingForPlayerAction = true;
            }
        }

        public void AIAttackAction()
        {
            var shotHandler = _currentActiveUnit.GetComponentInChildren<ShotHandler>();
            shotHandler.Shot();
            StartCoroutine(nameof(WaitForTurn));
        }

        public void PlayerAttackAction()
        {
            if (!_waitingForPlayerAction) return;

            var shotHandler = _currentActiveUnit.GetComponentInChildren<ShotHandler>();
            var targetLockOn = _currentActiveUnit.GetComponent<TargetLockOn>();

            if (!targetLockOn.currentEnemy) return;

            shotHandler.Shot();
            _waitingForPlayerAction = false;
            StartCoroutine(nameof(WaitForTurn));
        }

        private IEnumerator WaitForTurn()
        {
            yield return new WaitForSeconds(1);
            Battle();
        }

        private void InitPlayers()
        {
            foreach (PlayerStats playerStats in _playerTeam)
            {
                _combatants.Add(playerStats);
                _remainingAllies.Add(playerStats);
            }
        }

        private void InitEnemies()
        {
            foreach (EnemyStats enemyStats in _enemyTeam)
            {
                _combatants.Add(enemyStats);
                _remainingEnemies.Add(enemyStats);
            }
        }
    }
}