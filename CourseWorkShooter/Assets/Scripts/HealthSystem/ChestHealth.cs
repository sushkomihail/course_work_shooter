using Chest;
using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem
{
    public class ChestHealth : Health
    {
        private ChestShaker _shaker;

        public static readonly UnityEvent<float> OnHealthChanged = new UnityEvent<float>();
        
        public void Initialize(ChestShaker shaker)
        {
            Initialize();
            
            _shaker = shaker;
        }
        
        public override void TakeDamage(int damage)
        {
            _shaker.SetUpShakeVariables();

            float realDamage = Mathf.Min(_currentHealth, damage);
            _currentHealth -= realDamage;
            float healthFraction = _currentHealth / _maxHealth;
            OnHealthChanged?.Invoke(healthFraction);

            if (_currentHealth <= 0)
            {
                GameManager.Instance.EndGame();
            }
        }
    }
}