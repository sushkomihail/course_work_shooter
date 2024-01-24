using HealthSystem;
using PauseSystem;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyVision _vision;
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private EnemyWeaponController _weaponController;
        [SerializeField] private EnemyHealth _health;

        private PauseManager _pauseManager => GameManager.Instance.PauseManager;

        private void Awake()
        {
            _vision.Initialize();
            _weaponController.Initialize();
            _movement.Initialize(_weaponController.AttackDistance);
            _health.Initialize();
            
            _pauseManager.AddHandler(_movement);
        }

        private void Update()
        {
            if (!_pauseManager.IsPaused)
            {
                _vision.Look();
                _weaponController.Control(_vision.CurrentTarget);
            }
            
            _movement.Move(_vision.CurrentTarget);
        }
    }
}
