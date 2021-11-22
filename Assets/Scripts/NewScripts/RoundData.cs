using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS
{
    [CreateAssetMenu(menuName = "GameData")]
    public class RoundData : ScriptableObject
    {
        [SerializeField] private bool endRound = false;
        [SerializeField] private int roundCount = 0;
        [SerializeField] private List<ISkill> skil;
        [SerializeField] private Skills skills;

        [SerializeField] private Dictionary<SkillType, Sprite> _skilsImages;
        public bool EndRound { get => endRound; set => endRound = value; }
        public int RoundCount { get => roundCount; set => roundCount = value; }
        public List<ISkill> Skil { get => skil; set => skil = value; }
        public Skills Skills { get => skills; set => skills = value; }

        //public Dictionary<SkillType, Sprite> SkilsImages
        //{
        //    get
        //    {
        //        var count = Enum.GetValues(typeof(SkillType)).Length;
        //        for (int i = 0; i < count; i++)
        //        {
        //            SkilsImages.Add(Extenshion.GetSkillFromEnum(i), images[i]);
        //        }
        //        return SkilsImages;
        //    }       
        //}     
    }
}