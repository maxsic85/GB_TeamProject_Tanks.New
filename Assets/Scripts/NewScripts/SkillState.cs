using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS
{
    public class SkillState
    {
        Skills skills;
        public event Action<SkillData> ChangeImageEvent = delegate (SkillData data) { };
        public SkillState()
        {
            skills = ServiceLocator.Resolve<GameStarter>().roundData.Skills;
            foreach (var skill in skills.SkillDatas)
            {
                skill.DelayRoundToActive = 0;
                skill.IsEnable = true;
            }
            ServiceLocator.SetService<SkillState>(this);
        }

        public void UpdateStateSkills()
        {
            foreach (var skill in skills.SkillDatas)
            {
                IncreaseCoolDown(skill);
                ChahgeIconState(skill);
            }
        }
        private void ChahgeIconState(SkillData skill)
        {
            ChangeImageEvent?.Invoke(skill);
        }
        private bool IncreaseCoolDown(SkillData skillData)
        {
            if (skillData.IsEnable == false)
            {
                ++skillData.DelayRoundToActive;
            }
            if (skillData.DelayRoundToActive == skillData.Cooldown)
            {
                skillData.IsEnable = true;
                skillData.DelayRoundToActive = 0;
            }
            return (skillData.DelayRoundToActive == 0) ? true : false;
        }
    }
}
