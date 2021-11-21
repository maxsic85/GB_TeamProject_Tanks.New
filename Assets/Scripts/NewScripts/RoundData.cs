using System.Collections.Generic;
using UnityEngine;
namespace AS
{
    [CreateAssetMenu(menuName = "GameData")]
    public class RoundData : ScriptableObject
    {
        [SerializeField] private bool endRound = false;
        [SerializeField] private int roundCount = 0;
        [SerializeField] private List<ISkill> skil;


        public bool EndRound { get => endRound; set => endRound = value; }
        public int RoundCount { get => roundCount; set => roundCount = value; }
        public List<ISkill> Skil { get => skil; set => skil = value; }
    }
}