using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class AssaultRifle : Weapon
    {
        [SerializeField] private int _fireRate;

        private float _timeBetweenShots;

        public override void Initialize(Camera camera)
        {
            _camera = camera;
            _attack = new RaycastAttack(_camera, _muzzle, _spreadRange, _attackMask, _damage);
            _timeBetweenShots = 60.0f / _fireRate;
        }

        public override IEnumerator PerformAttack()
        {
            _canAttack = false;
            _attack.PerformAttack();
            yield return new WaitForSeconds(_timeBetweenShots);
            _canAttack = true;
        }
    }
}