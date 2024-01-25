using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Enemy/EnemiesCollection", fileName = "EnemiesCollection")]
    public class EnemiesCollection : ScriptableObject
    {
        [SerializeField] private EnemyController[] _enemies;

        private int _nextIndex;

        public EnemyController NextEnemy
        {
            get
            {
                if (_nextIndex > _enemies.Length - 1)
                {
                    _nextIndex = 0;
                }
                
                return _enemies[_nextIndex++];
            }
        }
    }
}