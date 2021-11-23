using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AS
{
    public class EnemyStats : CharacterStats
    {
        [FormerlySerializedAs("enemyHealthBar")] public HealthBar _healthBar;
        public GameObject ExplosionFX;
        public GameObject SmokeFX;
       private void Start()
        {
            MaxHealth = SetMaxHealthFromHealthLevelFormula();
            CurrentHealth = MaxHealth;
            _healthBar.SetMaxHealth(MaxHealth);
            _healthBar.SetCurrentSkill(_skillType);
        }

        public static EnemyStats CreateTank(Transform enemyTransform, Transform healthBarTransform)
        {
            var enemy = Instantiate(Resources.Load<EnemyStats>("Prefabs/EnemyTanks"));
            enemy.transform.position=enemyTransform.transform.position;
            enemy.transform.rotation = enemyTransform.transform.rotation;

            var healthbar = Instantiate(Resources.Load<HealthBar>("Prefabs/HealthBar"));
            healthbar.transform.SetParent(healthBarTransform);
            healthbar.transform.position = enemy.transform.GetChild(2).position;
            healthbar.transform.localScale = Vector3.one;
            healthbar.transform.localRotation = Quaternion.AngleAxis(0,Vector3.zero);


            enemy._healthBar = healthbar;
            enemy.GetComponent<TankController>()._health = healthbar;
            return enemy;
        }

        private int SetMaxHealthFromHealthLevelFormula()
        {
            MaxHealth = HealthLevel * 3;
            return MaxHealth;
        }
        public void UpdateEnemyHealthSlider()
        {
            _healthBar.SetCurrentHealth(CurrentHealth);
        }
        public void HandleDeath()
        {
            var rotation = transform.rotation;
            var position = transform.position;
            
            Instantiate(ExplosionFX, position, rotation);
            Instantiate(SmokeFX, position, rotation);
            
            var targetLockOn = FindObjectOfType<TargetLockOn>();
            targetLockOn.ClearTarget();
        }
    }
}
