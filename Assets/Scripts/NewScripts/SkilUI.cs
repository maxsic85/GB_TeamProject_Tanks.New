
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AS
{
    public class SkilUI : MonoBehaviour
    {
        public SkillData skillData;
        private Image _sprite;
        private Button _button;
        private PlayerStats _player;

        void Start()
        {
            FindObjectOfType<PlayerStats>()?.TryGetComponent<PlayerStats>(out _player);
            _sprite = GetComponent<Image>();
            _button = gameObject.GetOrAddComponent<Button>();
            _sprite.sprite = skillData.ImageWhenActivSkill;
            gameObject.GetOrAddComponent<Button>().onClick.AddListener(SetSkillForPlayerByUI);
            ServiceLocator.Resolve<SkillState>().ChangeImageEvent += ChangeIcon;
            //  InvokeRepeating("ChangeIcon", 1, 1);
        }
        private void SetSkillForPlayerByUI()
        {
            ServiceLocatorMonoBehavior.GetService<PlayerStats>().UpdateSkill(skillData);
            _button.enabled = false;
            ChangeIcon(skillData);
        }

        private void ChangeIcon(SkillData _skillData)
        {

            if (skillData.IsEnable)
            {
                _sprite.sprite = skillData.ImageWhenActivSkill;
                _button.enabled = true;
            }
            else _sprite.sprite = skillData.ImageWhenBlockSkill;
        }


    }
}