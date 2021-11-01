using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class PlayerStats : CharacterStats
    {
        public PlayerHealthBar playerHealthBar;
        public GameObject ExplosionFX;
        public GameObject SmokeFX;
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevelFormula();
            currentHealth = maxHealth;
            playerHealthBar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevelFormula()
        {
            maxHealth = healthLevel * 15;
            return maxHealth;
        }
        public void UpdatePlayerHealthSlider()
        {
            playerHealthBar.SetCurrentHealth(currentHealth);
        }
        public void HandleDeath()
        {
            GameObject temp = Instantiate(ExplosionFX, transform.position, transform.rotation);
            GameObject temp1 = Instantiate(SmokeFX, transform.position, transform.rotation);
        }
    }
}
