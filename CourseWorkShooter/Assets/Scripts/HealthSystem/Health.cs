using UnityEngine;

namespace HealthSystem
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] protected int _maxHealth = 100;
        
        protected float _currentHealth;

        public bool IsDied { get; protected set; }

        public virtual void Initialize()
        {
            _currentHealth = _maxHealth;
        }

        public abstract void TakeDamage(int damage);
    }
}