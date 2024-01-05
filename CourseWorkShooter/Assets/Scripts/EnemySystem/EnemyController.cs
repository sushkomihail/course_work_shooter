using UnityEngine;

namespace EnemySystem
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _spawnYOffset;
        [SerializeField] private EnemyVision _vision;
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private EnemyAttackController _attackController;

        public float SpawnYOffset => _spawnYOffset;

        private void Awake()
        {
            _vision.Initialize();
            _attackController.Initialize();
            _movement.Initialize(_attackController.AttackDistance);
        }

        private void Update()
        {
            _movement.MoveToTarget(_vision);
            _attackController.UpdateAttack(_movement.CurrentTarget);
        }
    }
}
