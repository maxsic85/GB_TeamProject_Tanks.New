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
        CharacterStats _player;
        private Dictionary<SkillType, Action> _actions;

   
        private void Start()
        {
            _player = FindObjectOfType<PlayerStats>().GetComponent<CharacterStats>();
        }

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

        private void SetDamagRandom() => _player.GetComponentInChildren<ShotHandler>().JustShot(true);
        private void SetDamagToAll()
        {
            var enemies = CombatHandler.Instance.EnemyTeam;
            _player.GetComponentInChildren<ShotHandler>().ShotToAllEnemies();
        }
        private void SetDamagToTarget() => _player.GetComponentInChildren<ShotHandler>().JustShot(false);


    }
}