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
            _shaker.SetUpShakeVariables();
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                GameManager.Instance.EndGame();
            }
        }
    }
}