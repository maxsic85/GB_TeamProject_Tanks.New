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
