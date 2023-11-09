﻿using UnityEngine;

namespace WeaponSystem
{
    public abstract class Attack
    {
        protected LayerMask _attackMask;
        protected readonly int _damage;
        
        protected Attack(LayerMask attackMask, int damage)
        {
            _attackMask = attackMask;
            _damage = damage;
        }
        
        protected Vector3 CameraRayHitPosition(Camera camera, float attackDistance, Vector3 spreadRange)
        {
            Vector3 rayDirection = camera.transform.forward + CalculateSpread(spreadRange);
            Ray ray = new Ray(camera.transform.position, rayDirection);
            
            if (Physics.Raycast(ray, out RaycastHit hit, attackDistance))
            {
                return hit.point;
            }
            
            return camera.transform.position + rayDirection * attackDistance;
        }
        
        private Vector3 CalculateSpread(Vector2 spreadRange)
        {
            return new Vector3
            {
                x = Random.Range(-spreadRange.x, spreadRange.x),
                y = Random.Range(-spreadRange.y, spreadRange.y)
            };
        }
        
        public abstract void PerformAttack();
    }
}