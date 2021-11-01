using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AS
{
    public class CombatHandler : MonoBehaviour
    {
        private List<CharacterStats> Combatants;

        private CharacterStats[] PlayerTeam;
        private CharacterStats[] EnemyTeam;

        private List<CharacterStats> RemainingEnemies;
        private List<CharacterStats> RemainingAllies;

        private CharacterStats currentActiveUnit;
        [HideInInspector]
        public CharacterStats currentAIUnitTarget;

        private bool WaitingForPlayerAction;

        private void Start()
        {
            Combatants = new List<CharacterStats>();
            RemainingAllies = new List<CharacterStats>();
            RemainingEnemies = new List<CharacterStats>();

            PlayerTeam = FindObjectsOfType<PlayerStats>();
            foreach (PlayerStats playerStats in PlayerTeam)
            {
                Combatants.Add(playerStats);
                RemainingAllies.Add(playerStats);                
            }

            EnemyTeam = FindObjectsOfType<EnemyStats>();
            foreach (EnemyStats enemyStats in EnemyTeam)
            {
                Combatants.Add(enemyStats);
                RemainingEnemies.Add(enemyStats);
            }
            Battle();
        }
        public void Battle()
        {            
            if (RemainingAllies.Count == 0)
            {
                Debug.Log("Defeat");
            }
            if (RemainingEnemies.Count == 0)
            {
                Debug.Log("Victory");
            }
            else
            {
                currentActiveUnit = Combatants[0];
                Combatants.Remove(currentActiveUnit);
                Combatants.Add(currentActiveUnit);
                if (!currentActiveUnit.isDead)
                {
                    CheckingTarget();
                }
                else
                {
                    Debug.Log("Dead -> skip");
                    Combatants.Remove(currentActiveUnit);
                    if (currentActiveUnit.tag == "Enemy")
                    {
                        RemainingEnemies.Remove(currentActiveUnit);
                    }
                    else if (currentActiveUnit.tag == "Player")
                    {
                        RemainingAllies.Remove(currentActiveUnit);
                    }
                    Battle();
                }
            }
        }
        public void CheckingTarget()
        {
            if (currentActiveUnit.tag == "Enemy")
            {
                if (RemainingAllies.Count != 0)
                {
                    int index = Random.Range(0, RemainingAllies.Count);
                    currentAIUnitTarget = RemainingAllies[index];
                    AIAttackAction();
                }
            }
            else if (currentActiveUnit.tag == "Player")
            {
                WaitingForPlayerAction = true;
            }
        }
        public void AIAttackAction()
        {
            ShotHandler shotHandler = currentActiveUnit.GetComponentInChildren<ShotHandler>();
            shotHandler.Shot();
            StartCoroutine("WaitForTurn");
        }
        public void PlayerAttackAction()
        {
            if (WaitingForPlayerAction == true)
            {
                ShotHandler shotHandler = currentActiveUnit.GetComponentInChildren<ShotHandler>();
                shotHandler.Shot();
                WaitingForPlayerAction = false;
                StartCoroutine("WaitForTurn");
            }
        }
        IEnumerator WaitForTurn()
        {
            yield return new WaitForSeconds(1);
            Battle();
        }
    }
}   
