using UnityEngine;

namespace Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        public bool IsDied { get; private set; }
        
        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                IsDied = true;
            }
        }
    }
}