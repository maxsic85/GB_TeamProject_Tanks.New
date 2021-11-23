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

        public static PlayerStats CreatePlayer(Transform playerTransform, Transform healthBarTransform)
        {
            var enemy = Instantiate(Resources.Load<PlayerStats>("Prefabs/PlayerTank"));
            enemy.transform.position = playerTransform.transform.position;
            enemy.transform.rotation = playerTransform.transform.rotation;

            var healthbar = Instantiate(Resources.Load<HealthBar>("Prefabs/HealthBar"));
            healthbar.transform.SetParent(healthBarTransform);
            healthbar.transform.position = enemy.transform.GetChild(2).position;
            healthbar.transform.localScale = Vector3.one;
            healthbar.transform.localRotation = Quaternion.AngleAxis(0, Vector3.zero);


            enemy.playerHealthBar = healthbar;
            enemy.GetComponent<TankController>()._health = healthbar;
            return enemy;
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
