using System.Collections;
using AttackSystem;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class Shotgun : Weapon
    {
        [SerializeField] private int _shotsCount = 8;
        
        public override void Initialize(Transform cameraTransform, IRecoilControlAngles recoilControlAngles)
        {
            base.Initialize(cameraTransform, recoilControlAngles);

            _attack = new RaycastAttack(cameraTransform, _muzzle, _spreadRange, _attackMask, _damage);
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _attack = new RaycastAttack(transform, _muzzle, _spreadRange, _attackMask, _damage);
        }

        public override IEnumerator Shoot()
        {
            IsReadyToShoot = false;
            
            _effects.ShowShotEffects();
            
            for (int i = 0; i < _shotsCount; i++)
            {
                _attack.Perform();
                _effects.ShowBulletEffects(_muzzle, _attack);
            }
            
            yield return new WaitForSeconds(_shotCooldownTime);
            
            IsReadyToShoot = true;
        }

        public override IEnumerator Shoot(Transform target)
        {
            IsReadyToShoot = false;
            
            _effects.ShowShotEffects();
            
            for (int i = 0; i < _shotsCount; i++)
            {
                _attack.Perform(target);
                _effects.ShowBulletEffects(_muzzle, _attack);
            }
            
            yield return new WaitForSeconds(_shotCooldownTime);
            
            IsReadyToShoot = true;
        }
    }
}