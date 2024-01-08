using Chest;

namespace HealthSystem
{
    public class ChestHealth : Health
    {
        private ChestShaker _shaker;
        
        public void Initialize(ChestShaker shaker)
        {
            _shaker = shaker;
        }
        
        public override void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _shaker.SetUpShakeVariables();

            if (_currentHealth <= 0)
            {
            }
        }

        private void OnTakeDamege()
        {
            
        }
    }
}