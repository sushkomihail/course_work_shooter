using SpawnSystem;
using UnityEngine;

namespace HealthSystem
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] protected int _maxHealth;
        
        protected int _currentHealth;

        public bool IsDied { get; protected set; }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public abstract void TakeDamage(int damage);
    }
}