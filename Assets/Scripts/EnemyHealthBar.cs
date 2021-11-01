using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS
{
    public class EnemyHealthBar : MonoBehaviour
    {
        Slider slider;
        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
        }

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
        public void SetCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }
        private void Update()
        {
            if (slider != null)
            {
                if (slider.value <= 0)
                {
                    Destroy(slider.gameObject);
                }
            }
        }
    }
}
