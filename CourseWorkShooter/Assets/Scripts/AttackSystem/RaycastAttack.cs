using HealthSystem;
using UnityEngine;

namespace AttackSystem
{
    public class RaycastAttack : Attack
    {
        private readonly LayerMask _attackMask;
        private readonly Transform _cameraTransform;
        private readonly Transform _rayOrigin;
        private readonly Vector2 _spreadRange;

        public RaycastAttack(Transform cameraTransform, Transform rayOrigin, Vector2 spreadRange,
            LayerMask attackMask, int damage) : base(damage)
        {
            _attackMask = attackMask;
            _cameraTransform = cameraTransform;
            _spreadRange = spreadRange;
            _rayOrigin = rayOrigin;
        }

        public override void Perform()
        {
            CalculateHitPosition(_cameraTransform, _spreadRange);
            Vector3 directionToTarget = HitPosition - _rayOrigin.position;

            if (Physics.Raycast(_rayOrigin.position, directionToTarget, out RaycastHit hit,
                directionToTarget.magnitude, _attackMask.value))
            {
                if (!hit.transform.root.TryGetComponent(out Health health)) return;
                
                if (health.IsDied) return;

                health.TakeDamage(_damage);
            }
        }
    }
}
