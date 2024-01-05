using System.Collections.Generic;
using EnemySystem;

namespace SpawnSystem
{
    public class Wave
    {
        private readonly int _enemiesCount;
        private readonly WaveType _type;
        
        private Dictionary<WaveType, float> variablesMultiplier = new Dictionary<WaveType, float>();
        
        public Wave(int enemiesCount, WaveType type)
        {
            _enemiesCount = enemiesCount;
            _type = type;
        }

        public void GetUpgradedEnemies(EnemyController[] enemies)
        {
            
        }
    }
}