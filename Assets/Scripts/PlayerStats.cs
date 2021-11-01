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
            MaxHealth = SetMaxHealthFromHealthLevelFormula();
            CurrentHealth = MaxHealth;
            playerHealthBar.SetMaxHealth(MaxHealth);
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
