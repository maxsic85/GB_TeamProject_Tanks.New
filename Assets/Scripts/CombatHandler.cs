using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

namespace AS
{
    public class CombatHandler : MonoBehaviour
    {
        public RoundData _roundData;
        [SerializeField] private int _roundCount = 0;
        [SerializeField] private bool _endRound = false;
        ISkill _skill;

        private List<CharacterStats> _combatants;
        private PlayerStats[] _playerTeam;
        private EnemyStats[] _enemyTeam;
        private List<CharacterStats> _remainingEnemies;
        private List<CharacterStats> _remainingAllies;
        //private List<AbilityComponent> abilityComponents;

        private CharacterStats _currentActiveUnit;
        [HideInInspector] public CharacterStats _currentAIUnitTarget;

        private bool _waitingForPlayerAction;

        private static CombatHandler _instance;
        public static CombatHandler Instance { get { return _instance; } }

        public EnemyStats[] EnemyTeam { get => _enemyTeam; set => _enemyTeam = value; }
        public PlayerStats[] PlayerTeam { get => _playerTeam; set => _playerTeam = value; }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;

            }

            _skill = gameObject.GetOrAddComponent<Skill>();
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
            SetSkills();
            InvokeRepeating(nameof(CheckNewRound), 1, 0.2f);
            Battle();
        }

        public void Battle()
        {

            if (_endRound) return;
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

                    //Battle();
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
            shotHandler.Shot(_currentActiveUnit);
            _currentActiveUnit.IsEndRound = true;
            StartCoroutine(nameof(WaitForTurn));
        }

        public void PlayerAttackAction()
        {
            if (!_waitingForPlayerAction) return;

            var shotHandler = _currentActiveUnit.GetComponentInChildren<ShotHandler>();
            var targetLockOn = _currentActiveUnit.GetComponent<TargetLockOn>();

            if (!targetLockOn.currentEnemy) return;

            shotHandler.Shot(_currentActiveUnit);
            _waitingForPlayerAction = false;
            _currentActiveUnit.IsEndRound = true;
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

        private void SetSkills()
        {
            var count = Enum.GetValues(typeof(SkillType)).Length;
            foreach (var tanks in _combatants)
            {
                int rnd = Random.Range(0, count);
                tanks._SkillType = tanks.GetRandomSkill(rnd);
                tanks.Skill = _skill;
            }
        }

        private void CheckNewRound()
        {
            if (CheckEndRound() && _roundData.EndRound)
            {
                foreach (var item in _combatants)
                {
                    item.IsEndRound = false;
                }
                _endRound = false;
                _roundCount = _roundCount + 1;
                _roundData.EndRound = false;
                _roundData.RoundCount = _roundCount;
                SetSkills();
                Battle();

            }
        }
        private bool CheckEndRound()
        {
            int number = (from t in _combatants where !t.IsEndRound select t).Count();
            _endRound = (number == 0) ? true : false;

            return _endRound;
        }


    }
}