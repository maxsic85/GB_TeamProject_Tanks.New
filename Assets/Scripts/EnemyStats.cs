using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class EnemyStats : CharacterStats
    {
        public EnemyHealthBar enemyHealthBar;
        public GameObject ExplosionFX;
        public GameObject SmokeFX;
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevelFormula();
            currentHealth = maxHealth;
            enemyHealthBar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevelFormula()
        {
            maxHealth = healthLevel * 3;
            return maxHealth;
        }
        public void UpdateEnemyHealthSlider()
        {
            enemyHealthBar.SetCurrentHealth(currentHealth);
        }
        public void HandleDeath()
        {
            GameObject temp = Instantiate(ExplosionFX, transform.position, transform.rotation);
            GameObject temp1 = Instantiate(SmokeFX, transform.position, transform.rotation);
            TargetLockOn targetLockOn = FindObjectOfType<TargetLockOn>();
            targetLockOn.ClearTarget();
        }
    }
}
