using UnityEngine;

namespace AS
{
    public class CharacterStats : MonoBehaviour
    {
       [SerializeField] private int _healthLevel = 10;
       [SerializeField] private int _maxHealth;

        [SerializeField] private bool _isUseSkiil = false;

        [SerializeField] ISkill skill;
        [SerializeField] SkillType _skillType;
        [SerializeField] ShotType _shotType;

        [SerializeField] private bool _isEndRound = false;

        private int _currentHealth;
        private bool _isDead;



        public int HealthLevel
        {
            get => _healthLevel;
            set => _healthLevel = value;
        }

        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public bool IsDead
        {
            get => _isDead;
            set => _isDead = value;
        }

        public bool IsEndRound
        {
            get => _isEndRound;
            set => _isEndRound = value;
        }

        public bool IsUseSkill
        {
            get => _isUseSkiil;
            set => _isUseSkiil = value;
        }
        public ISkill Skill { get => skill; set => skill = value; }
        public SkillType _SkillType { get => _skillType; set => _skillType = value; }
        public ShotType ShotType { get => _shotType; set => _shotType = value; }

        internal SkillType GetRandomSkill(int index)
        {
            _skillType = index switch
            {
                0 => SkillType.FIRE,
                1 => SkillType.WATER,
                2 => SkillType.EARTH,
                _ => 0
            };
            return _skillType;
        }

        public void TakingDamage(int damage)
        {
            if (_isDead) return;
            _currentHealth = _currentHealth - damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                IsDead = true;
            }
        }
    }
}

