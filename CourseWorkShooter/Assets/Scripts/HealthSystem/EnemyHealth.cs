using SpawnSystem;

namespace HealthSystem
{
    public class EnemyHealth : Health
    {
        public override void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                IsDied = true;
                SpawnController.OnEnemyDeath.Invoke(gameObject);
            }
        }
    }
}