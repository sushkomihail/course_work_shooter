using System.Collections;
using AttackSystem;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class AssaultRifle : Weapon
    {
        public override void Initialize(Transform cameraTransform, ICameraAngles cameraAngles)
        {
            base.Initialize(cameraTransform, cameraAngles);

            _attack = new RaycastAttack(cameraTransform, _muzzle, _spreadRange, _attackMask, _damage);
        }

        public override void Initialize()
        {
            _attack = new RaycastAttack(transform, _muzzle, _spreadRange, _attackMask, _damage);
        }

        public override IEnumerator PerformAttack()
        {
            _isReadyToShoot = false;
            Shoot();
            yield return new WaitForSeconds(_shotCooldownTime);
            _isReadyToShoot = true;
        }
    }
}