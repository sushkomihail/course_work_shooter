using Extensions;
using UnityEngine;
using WeaponSystem;

namespace Enemy
{
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private Transform _aimingTarget;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _aimingTargetSpeed = 5;

        public float AttackDistance => _attackDistance;

        public void Initialize()
        {
            _weapon.Initialize();
        }

        public void UpdateAttack(Transform currentTarget)
        {
            AimToTarget(currentTarget);
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
            
            if (distanceToTarget <= _attackDistance)
            {
                if (_weapon.IsReadyToShoot)
                {
                    StartCoroutine(_weapon.Shoot(_aimingTarget));
                }
            }
        }

        private void AimToTarget(Transform currentTarget)
        {
            _aimingTarget.position = 
                Vector3.Lerp(_aimingTarget.position, currentTarget.position, _aimingTargetSpeed * Time.deltaTime);

            if (_aimingTarget.position.IsComparableWith(currentTarget.position))
            {
                _aimingTarget.position = currentTarget.position;
            }

            if (_aimingTarget.position == currentTarget.position)
            {
                Vector3 weaponDirectionToTarget = currentTarget.position - _weapon.transform.position;
                _weapon.transform.rotation = Quaternion.RotateTowards(
                    _weapon.transform.rotation,
                    Quaternion.LookRotation(weaponDirectionToTarget),
                    _aimingTargetSpeed * Time.deltaTime);
            }
        }
    }
}