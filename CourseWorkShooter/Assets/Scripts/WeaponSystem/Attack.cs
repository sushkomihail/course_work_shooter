using UnityEngine;

namespace WeaponSystem
{
    public abstract class Attack
    {
        protected LayerMask _attackMask;
        protected readonly int _damage;

        protected Vector3 _hitPosition;
        
        private const int RayDistanceWithoutHit = 200;

        public Vector3 HitPosition => _hitPosition;

        protected Attack(LayerMask attackMask, int damage)
        {
            _attackMask = attackMask;
            _damage = damage;
        }
        
        public abstract void PerformAttack();
        
        protected void CalculateHitPosition(Camera camera, Vector3 spreadRange)
        {
            Vector3 rayDirection = camera.transform.forward + CalculateSpread(spreadRange);
            Ray ray = new Ray(camera.transform.position, rayDirection);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                _hitPosition = hit.point;
                return;
            }
            
            _hitPosition = camera.transform.position + rayDirection * RayDistanceWithoutHit;
        }
        
        private Vector3 CalculateSpread(Vector2 spreadRange)
        {
            return new Vector3
            {
                x = Random.Range(-spreadRange.x, spreadRange.x),
                y = Random.Range(-spreadRange.y, spreadRange.y)
            };
        }
    }
}