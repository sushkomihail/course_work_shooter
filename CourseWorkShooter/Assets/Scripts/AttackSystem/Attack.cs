using UnityEngine;

namespace AttackSystem
{
    public abstract class Attack
    {
        protected readonly int _damage;
        private const int RayDistanceWithoutHit = 200;

        public bool IsHit { get; private set; }
        public Vector3 HitPosition { get; private set; }
        public Vector3 HitNormal { get; private set; }
        public LayerMask HitLayer { get; private set; }

        protected Attack(int damage)
        {
            _damage = damage;
        }

        public abstract void Perform();
        public abstract void Perform(Transform target);
        
        protected void CalculateHitPosition(Transform rayOrigin, Vector2 spreadRange)
        {
            Vector3 spread = CalculateSpread(spreadRange);

            Vector3 rayDirection = rayOrigin.forward + 
                                   rayOrigin.right * spread.x + 
                                   rayOrigin.up * spread.y;
            
            CastRay(rayOrigin.position, rayDirection);
        }
        
        protected void CalculateHitPosition(Transform rayOrigin, Transform target, Vector2 spreadRange)
        {
            Vector3 spread = CalculateSpread(spreadRange);

            Vector3 rayDirection = target.position - rayOrigin.position;
            rayDirection.x += spread.x;
            rayDirection.y += spread.y;
            
            CastRay(rayOrigin.position, rayDirection);
        }

        private void CastRay(Vector3 originPosition, Vector3 rayDirection)
        {
            Ray ray = new Ray(originPosition, rayDirection);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                IsHit = true;
                HitPosition = hit.point;
                HitNormal = hit.normal;
                HitLayer = hit.transform.gameObject.layer;
                return;
            }

            IsHit = false;
            HitPosition = originPosition + rayDirection * RayDistanceWithoutHit;
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