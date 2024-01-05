using UnityEngine;

namespace AttackSystem
{
    public class ProjectileAttack : Attack
    {
        private readonly Transform _muzzle;
        private readonly Vector2 _spreadRange;
        private readonly Projectile _projectilePrefab;
        private readonly float _force;

        public ProjectileAttack(Transform muzzle, Vector2 spreadRange, Projectile projectilePrefab,
            float force, int damage) : base(damage)
        {
            _muzzle = muzzle;
            _spreadRange = spreadRange;
            _projectilePrefab = projectilePrefab;
            _force = force;
        }

        public override void Perform()
        {
            CalculateHitPosition(_muzzle, _spreadRange);
            Vector3 directionToTarget = HitPosition - _muzzle.position;
            _projectilePrefab.Initialize(directionToTarget, _force, _damage);
        }
    }
}