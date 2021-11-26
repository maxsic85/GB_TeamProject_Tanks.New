using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class PlayerStats : CharacterStats, IInitialisation
    {
        public HealthBar playerHealthBar;
        public GameObject ExplosionFX;
        public GameObject SmokeFX;
       
        private GameObject _explosion;
        private GameObject _smoke;

        void Start()
        {
           
            _smoke = Instantiate(SmokeFX, transform.position, transform.rotation);
           
            _smoke.SetActive(false);
            Initialisation();

        }
        public void UpdateSkill(SkillData skillData)
        {
            playerHealthBar.SetCurrentSkill( skillData);
            _currentSkillData = skillData;
            UpdatePlayerHealthSlider();
        }
        private int SetMaxHealthFromHealthLevelFormula()
        {
            MaxHealth = HealthLevel * 15;
            return MaxHealth;
        }
        public void UpdatePlayerHealthSlider()
        {
            playerHealthBar.SetCurrentHealth(CurrentHealth);
        }
        public void HandleDeath()
        {
            _explosion = Instantiate(ExplosionFX, transform.position, transform.rotation);
            _smoke.SetActive(true);
        }

        public void Initialisation()
        {
            IsDead = false;
            _smoke.SetActive(false);

            ServiceLocatorMonoBehavior.GetService<PlayerStats>();
            MaxHealth = SetMaxHealthFromHealthLevelFormula();
            CurrentHealth = MaxHealth;
            playerHealthBar.SetMaxHealth(MaxHealth);
            UpdateSkill(_currentSkillData);

        }
    }
}
