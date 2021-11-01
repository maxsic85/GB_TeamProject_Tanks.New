using UnityEngine;

namespace AS
{
    public class CharacterStats : MonoBehaviour
    {
       [SerializeField] private int _healthLevel = 10;
       [SerializeField] private int _maxHealth;
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

