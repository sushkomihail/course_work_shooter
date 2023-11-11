using UnityEngine;

namespace WeaponSystem
{
    public class RaycastAttack : Attack
    {
        private readonly Camera _camera;
        private readonly Transform _rayOrigin;
        private readonly Vector2 _spreadRange;

        public RaycastAttack(Camera camera, Transform rayOrigin, Vector2 spreadRange,
            LayerMask attackMask, int damage) : base(attackMask, damage)
        {
            _camera = camera;
            _spreadRange = spreadRange;
            _rayOrigin = rayOrigin;
        }

        public override void PerformAttack()
        {
            Vector3 cameraRayHitPosition = CameraRayHitPosition(_camera, _spreadRange);
            Vector3 directionToTarget = cameraRayHitPosition - _rayOrigin.position;

            if (Physics.Raycast(_rayOrigin.position, directionToTarget, out RaycastHit hit,
                directionToTarget.magnitude, _attackMask.value))
            {
                if (hit.transform.root.TryGetComponent(out Health.Health health))
                {
                    if (health.IsDied) return;

                    health.TakeDamage(_damage);
                }
            }
        }
    }
}
