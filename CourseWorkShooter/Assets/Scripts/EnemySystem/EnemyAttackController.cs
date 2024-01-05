using UnityEngine;
using WeaponSystem;

namespace EnemySystem
{
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _attackDistance;

        public float AttackDistance => _attackDistance;

        public void Initialize()
        {
            _weapon.Initialize();
        }

        public void UpdateAttack(Transform currentTarget)
        {
            Vector3 directionToTarget = currentTarget.position - _weapon.transform.position;
            _weapon.transform.rotation = Quaternion.LookRotation(directionToTarget);
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= _attackDistance && _weapon.IsReadyToShoot)
            {
                StartCoroutine(_weapon.PerformAttack());
                _weapon.View.PlayMuzzleFlashParticles();
                _weapon.View.PlayShootSound();
            }
        }
    }
}