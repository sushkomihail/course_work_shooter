using UnityEngine;

namespace WeaponSystem
{
    public abstract class Attack
    {
        protected LayerMask _attackMask;
        protected readonly int _damage;
        private const int RayDistanceWithoutHit = 200;

        public Vector3 HitPosition { get; private set; }
        
        public Vector3 HitNormal { get; private set; }
        
        public bool IsHit { get; private set; }

        protected Attack(LayerMask attackMask, int damage)
        {
            _attackMask = attackMask;
            _damage = damage;
        }
        
        public abstract void Perform();
        
        protected void CalculateHitPosition(Camera camera, Vector3 spreadRange)
        {
            Transform cameraTransform = camera.transform;
            Vector3 spread = CalculateSpread(spreadRange);
            Vector3 rayDirection = cameraTransform.forward + 
                                   cameraTransform.right * spread.x + 
                                   cameraTransform.up * spread.y;
            Ray ray = new Ray(cameraTransform.position, rayDirection);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                IsHit = true;
                HitPosition = hit.point;
                HitNormal = hit.normal;
                return;
            }

            IsHit = false;
            HitPosition = cameraTransform.position + rayDirection * RayDistanceWithoutHit;
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