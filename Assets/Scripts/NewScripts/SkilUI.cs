
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AS
{
    public class SkilUI : MonoBehaviour
    {
        public SkillData skillData;
        private Image image;
        private PlayerStats _player;

        void Start()
        {
            FindObjectOfType<PlayerStats>()?.TryGetComponent<PlayerStats>(out _player);
            image = GetComponent<Image>();
            image.sprite = skillData.Image;
            gameObject.GetOrAddComponent<Button>().onClick.AddListener(SetSkillForPlayerByUI);
            ServiceLocator.Resolve<SkillState>().ChangeImageEvent += ChangeIcon;
            InvokeRepeating("ChangeIcon", 1, 1);
        }
        private void SetSkillForPlayerByUI()
        {
            ServiceLocatorMonoBehavior.GetService<PlayerStats>().UpdateSkill(skillData);
           // ChangeIcon(skillData);
        }

        private void ChangeIcon(SkillData _skillData)
        {
           
            if (skillData.IsEnable) gameObject.GetComponent<Image>().sprite = skillData.Image;
            else gameObject.GetComponent<Image>().sprite = null;
        }

     
    }
}