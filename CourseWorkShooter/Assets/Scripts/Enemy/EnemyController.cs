using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyVision _vision;
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private EnemyAttackController _attackController;

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
