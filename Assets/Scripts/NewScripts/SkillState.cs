using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class SkillState 
    {
        Skills skills;

        public SkillState()
        {
            skills = ServiceLocator.Resolve<GameStarter>().roundData.Skills;
            foreach (var skill in skills.SkillDatas)
            {
                skill.DelayRoundToActive = 0;
            }
            ServiceLocator.SetService<SkillState>(this);
        }

        public void UpdateStateSkills()
        {
            foreach (var skill in skills.SkillDatas)
            {    
                   IncreaseCoolDown(skill);            
            }
        }

        private bool IncreaseCoolDown(SkillData skillData)
        {
            if (skillData.IsEnable==false)
            {
                skillData.DelayRoundToActive++;
            }
            if (skillData.DelayRoundToActive == skillData.Cooldown)
            {
                skillData.IsEnable = true;
                skillData.DelayRoundToActive = 0;
            }
            return (skillData.DelayRoundToActive == 0)?true:false;
        }
    }
}
