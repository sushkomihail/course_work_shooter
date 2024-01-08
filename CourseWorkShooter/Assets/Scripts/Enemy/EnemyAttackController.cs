using UnityEngine;
using WeaponSystem;

namespace Enemy
{
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private Transform _aimingTarget;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _attackDistance;

        public float AttackDistance => _attackDistance;

        public void Initialize()
        {
            _weapon.Initialize();
        }

        public void UpdateAttack(Transform currentTarget)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= _attackDistance)
            {
                _aimingTarget.position = currentTarget.position;

                if (_weapon.IsReadyToShoot)
                {
                    StartCoroutine(_weapon.PerformAttack(_aimingTarget));
                    _weapon.View.PlayMuzzleFlashParticles();
                    _weapon.View.PlayShootSound();
                }
            }
        }
    }
}