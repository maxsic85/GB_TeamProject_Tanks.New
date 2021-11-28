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

        [SerializeField] private int _roundCount = 0;
        [SerializeField] private bool _endRound = false;
        ISkill _skill;

        private List<CharacterStats> _combatants;
        private PlayerStats[] _playerTeam;
        private EnemyStats[] _enemyTeam;
        private List<CharacterStats> _remainingEnemies;
        private List<CharacterStats> _remainingAllies;
        //private List<AbilityComponent> abilityComponents;

        private VictoryWindow _victoryWindow;
        private DefeatWindow _defeatWindow;

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
            ServiceLocator.Resolve<GameStarter>().roundData.EndRound = false;
            ServiceLocator.Resolve<GameStarter>().roundData.RoundCount = 0;
            _skill = gameObject.GetOrAddComponent<Skill>();
            _combatants = new List<CharacterStats>();
            _remainingAllies = new List<CharacterStats>();
            _remainingEnemies = new List<CharacterStats>();
            _playerTeam = FindObjectsOfType<PlayerStats>();
            _enemyTeam = FindObjectsOfType<EnemyStats>();
            _victoryWindow = FindObjectOfType<VictoryWindow>();
            _victoryWindow.gameObject.SetActive(false);
            _defeatWindow = FindObjectOfType<DefeatWindow>();
            _defeatWindow.gameObject.SetActive(false);

            InitPlayers();
            InitEnemies();
            SeRandomSkillsOnStart();
        }

        private void Start()
        {

            InvokeRepeating(nameof(CheckNewRound), 1, 0.2f);
            Battle();
        }

        public void Battle()
        {

            if (_endRound) return;
            if (_remainingAllies.Count == 0)
            {
                Debug.Log("Defeat");
               // _defeatWindow.gameObject.SetActive(true);
            }

            if (_remainingEnemies.Count == 0)
            {

                Debug.Log("Victory");
                _victoryWindow.gameObject.SetActive(true);


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
                        _defeatWindow.gameObject.SetActive(true);
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
            shotHandler.Shot(_currentActiveUnit, null);
            _currentActiveUnit.IsEndRound = true;
            StartCoroutine(nameof(WaitForTurn));
        }
        public void PlayerAttackAction()
        {
            if (!_waitingForPlayerAction) return;

            var shotHandler = _currentActiveUnit.GetComponentInChildren<ShotHandler>();
            //     var targetLockOn = _currentActiveUnit.GetComponent<TargetLockOn>();
            var targetLockOn = ServiceLocator.Resolve<GameStarter>().targetLockOn;

            if (!targetLockOn.currentEnemy) return;

            shotHandler.Shot(_currentActiveUnit, _currentActiveUnit._currentSkillData);
            _waitingForPlayerAction = false;
            _currentActiveUnit.IsEndRound = true;
            StartCoroutine(nameof(WaitForTurn));
        }
        private IEnumerator WaitForTurn()
        {
            yield return new WaitForSeconds(1);
            Battle();
        }
        private IEnumerator WaitForEndRound()
        {
            yield return new WaitForSeconds(1);
            ServiceLocator.Resolve<GameStarter>().roundData.EndRound = true;
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
        private void SeRandomSkillsOnStart()
        {
            var count = Enum.GetValues(typeof(SkillType)).Length;
            foreach (var tanks in _combatants)
            {
                int rnd = Random.Range(0, count);
                //  tanks._SkillType = tanks.GetRandomSkill(rnd);
                tanks._currentSkillData = tanks.GetRandomSkillData(rnd);
                tanks.Skill = _skill;
            }
        }
        private void CheckNewRound()
        {
            if (CheckEndRound())// && ServiceLocatorMonoBehavior.GetService<GameService>().roundData.EndRound)
            {

                if (ServiceLocator.Resolve<GameStarter>().roundData.EndRound)
                {

                    foreach (var item in _combatants)
                    {
                        item.IsEndRound = false;
                    }
                    _endRound = false;
                    _roundCount = _roundCount + 1;
                    ServiceLocator.Resolve<GameStarter>().roundData.EndRound = false;
                    ServiceLocator.Resolve<GameStarter>().roundData.RoundCount = _roundCount;
                    StopCoroutine(nameof(WaitForEndRound));
                    ServiceLocator.Resolve<SkillState>().UpdateStateSkills();
                    //reset skill to earth after player shot 
                    _playerTeam[0]._currentSkillData = _playerTeam[0].GetRandomSkillData(2);
                    ServiceLocatorMonoBehavior.GetService<PlayerStats>().UpdateSkill(_playerTeam[0]._currentSkillData);
                    Battle();
                }
                else if (_endRound)
                {
                    StartCoroutine(nameof(WaitForEndRound));
                }
            }
        }
        private bool CheckEndRound()
        {
            int number = (from t in _combatants where !t.IsEndRound select t).Count();
            _endRound = (number == 0) ? true : false;

            return _endRound;
        }
        public void RestartFight()
        {
            _victoryWindow.gameObject.SetActive(false);
            ServiceLocator.Resolve<GameStarter>().roundData.EndRound = false;
            ServiceLocator.Resolve<GameStarter>().roundData.RoundCount = 0;
            _combatants.Clear();
            _remainingAllies.Clear();
            _remainingEnemies.Clear();
            InitPlayers();

            InitEnemies();

            foreach (IInitialisation i in _combatants)
            {
                i.Initialisation();
            }
            Battle();
        }
    }
}