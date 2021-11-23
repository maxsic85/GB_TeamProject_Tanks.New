using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class PlayerStats : CharacterStats
    {
        public HealthBar playerHealthBar;
        public GameObject ExplosionFX;
        public GameObject SmokeFX;
        void Start()
        {
            ServiceLocatorMonoBehavior.GetService<PlayerStats>();
            MaxHealth = SetMaxHealthFromHealthLevelFormula();
            CurrentHealth = MaxHealth;
            playerHealthBar.SetMaxHealth(MaxHealth);
            UpdateSkill();
        }
        public void UpdateSkill()
        {
            playerHealthBar.SetCurrentSkill(_skillType);
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
            GameObject temp = Instantiate(ExplosionFX, transform.position, transform.rotation);
            GameObject temp1 = Instantiate(SmokeFX, transform.position, transform.rotation);
        }
    }
}
