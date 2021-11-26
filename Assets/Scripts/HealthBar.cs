using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS
{
    public class HealthBar : MonoBehaviour
    {
        private Slider _slider;
        private Image _image;
      

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            _image = transform.GetChild(1).GetComponent<Image>();

        }

        public void SetMaxHealth(int maxHealth)
        {
            _slider.maxValue = maxHealth;
            _slider.value = maxHealth;
            
            if (!isActiveAndEnabled)
                gameObject.SetActive(true);

        }
        public void SetCurrentHealth(int currentHealth)
        {
            if (_slider.value <= 0)
            {
                gameObject.SetActive(false);
            }
            _slider.value = currentHealth;
        }
        public void SetCurrentSkill(SkillData skillData)
        {
            if (skillData == null) return;
            _image.sprite = Extenshion.GetSpriteBySkillType(skillData.SkillType);
        }

    }
}
