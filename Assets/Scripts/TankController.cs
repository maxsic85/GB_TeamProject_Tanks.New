using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AS
{
    public class TankController : MonoBehaviour
    {
        [SerializeField] private TankView _tankView;
      public  HealthBar _health;     

        private void Start()
        {
            SetHealthBarPosition();
          
           // _skillType = gameObject.GetComponent<CharacterStats>()._SkillType;
            //skill.ExecuteSkill(GetRandomSkill(_random.Next(0,3)));
        }
        private void SetHealthBarPosition()
        {
           _health.transform.position = _tankView.HealthBarPosition;      
        }
    }
}