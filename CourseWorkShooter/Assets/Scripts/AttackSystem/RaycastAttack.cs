using HealthSystem;
using UnityEngine;

namespace AttackSystem
{
    public class RaycastAttack : Attack
    {
        private readonly LayerMask _attackMask;
        private readonly Transform _cameraTransform;
        private readonly Transform _muzzle;
        private readonly Vector2 _spreadRange;

        public RaycastAttack(Transform cameraTransform, Transform muzzle, Vector2 spreadRange,
            LayerMask attackMask, int damage) : base(damage)
        {
            _attackMask = attackMask;
            _cameraTransform = cameraTransform;
            _spreadRange = spreadRange;
            _muzzle = muzzle;
        }
        
        public RaycastAttack(Transform muzzle, Vector2 spreadRange,
            LayerMask attackMask, int damage) : base(damage)
        {
            _attackMask = attackMask;
            _spreadRange = spreadRange;
            _muzzle = muzzle;
        }

        public override void Perform()
        {
            CalculateHitPosition(_cameraTransform, _spreadRange);
            CastRay();
        }

        public override void Perform(Transform target)
        {
            CalculateHitPosition(_muzzle, target, _spreadRange);
            CastRay();
        }

        private void CastRay()
        {
            Vector3 directionToTarget = HitPosition - _muzzle.position;

            if (Physics.Raycast(_muzzle.position, directionToTarget, out RaycastHit hit,
                directionToTarget.magnitude, _attackMask.value))
            {
                if (!hit.transform.root.TryGetComponent(out Health health)) return;
                
                if (health.IsDied) return;

                health.TakeDamage(_damage);
            }
        }
    }
}
