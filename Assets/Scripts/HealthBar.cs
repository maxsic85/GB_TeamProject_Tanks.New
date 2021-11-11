using UnityEngine;
using UnityEngine.UI;

namespace AS
{
    public class HealthBar : MonoBehaviour
    {
        private Slider _slider;
        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }

        public void SetMaxHealth(int maxHealth)
        {
            _slider.maxValue = maxHealth;
            _slider.value = maxHealth;
        }
        public void SetCurrentHealth(int currentHealth)
        {
            if (_slider.value <= 0)
            {
                gameObject.SetActive(false);
            }
            _slider.value = currentHealth;
        }
    }
}
