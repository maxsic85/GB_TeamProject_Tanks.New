using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AS
{
    public interface ISkill
    {
        SkillType skillTtype { get; set; }
        void ExecuteSkill(SkillType skill);
    }

    public class Skill : MonoBehaviour, ISkill
    {
        SkillType ISkill.skillTtype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private Dictionary<SkillType, Action> _actions;

        public event Action ExecuteBonus;

        public void ExecuteSkill(SkillType value)
        {
            _actions = new Dictionary<SkillType, Action>
            {
                [SkillType.FIRE] = SetDamagToTarget,
                [SkillType.WATER] = SetDamagToAll,
                [SkillType.EARTH] = SetDamagRandom,

            };
            _actions[value]?.Invoke();
        }

        private void SetDamagRandom() => Debug.Log("EARTH");
        private void SetDamagToAll()
        {
            var enemies = CombatHandler.Instance.EnemyTeam;
            var player = FindObjectOfType<TargetLockOn>().GetComponent<CharacterStats>();
            player.GetComponentInChildren<ShotHandler>().ShotToAllEnemies();
        }
        private void SetDamagToTarget() => Debug.Log("FIRE");


    }
}