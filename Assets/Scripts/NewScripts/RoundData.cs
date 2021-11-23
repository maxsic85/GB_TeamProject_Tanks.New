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
        [SerializeField] private int enemyCntOnStart = 3;
        [SerializeField] private List<TransformsData> transformForInstantiateEnemy;
        [SerializeField] private TransformsData transformForInstantiatePlayer;

        [SerializeField] private Dictionary<SkillType, Sprite> _skilsImages;
        public bool EndRound { get => endRound; set => endRound = value; }
        public int RoundCount { get => roundCount; set => roundCount = value; }
        public List<ISkill> Skil { get => skil; set => skil = value; }
        public Skills Skills { get => skills; set => skills = value; }
        public int EnemyCntOnStart { get => enemyCntOnStart; set => enemyCntOnStart = value; }
        public List<TransformsData> TransformForInstantiateEnemy { get => transformForInstantiateEnemy; set => transformForInstantiateEnemy = value; }
        public TransformsData TransformForInstantiatePlayer { get => transformForInstantiatePlayer; set => transformForInstantiatePlayer = value; }
    }
}