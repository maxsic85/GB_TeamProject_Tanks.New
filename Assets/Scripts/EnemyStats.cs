using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AS
{
    public class EnemyStats : CharacterStats, IInitialisation
    {
        [FormerlySerializedAs("enemyHealthBar")] public HealthBar _healthBar;
        public GameObject ExplosionFX;
        public GameObject SmokeFX;

        private GameObject _explosion;
        private GameObject _smoke;

        private void Start()
        {          
            _smoke = Instantiate(SmokeFX, transform.position, transform.rotation);
            _smoke.SetActive(false);
            Initialisation();
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
            _smoke.SetActive(true);
            _explosion = Instantiate(ExplosionFX, transform.position, transform.rotation);


            var targetLockOn = FindObjectOfType<TargetLockOn>();
            targetLockOn.ClearTarget();
        }

        public void Initialisation()
        {
            IsDead = false;
            _smoke.SetActive(false);
            MaxHealth = SetMaxHealthFromHealthLevelFormula();
            CurrentHealth = MaxHealth;
            _healthBar.SetMaxHealth(MaxHealth);
            _healthBar.SetCurrentSkill(this._currentSkillData);

        }
    }
}
