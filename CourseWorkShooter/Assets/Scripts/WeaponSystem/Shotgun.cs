using System.Collections;
using AttackSystem;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class Shotgun : Weapon
    {
        [SerializeField] private int _shotsCount;
        
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
            
            for (int i = 0; i < _shotsCount; i++)
            {
                Shoot();
            }
            
            yield return new WaitForSeconds(_shotCooldownTime);
            _isReadyToShoot = true;
        }
    }
}