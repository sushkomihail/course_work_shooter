using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class AssaultRifle : Weapon
    {
        public override void Initialize(Camera camera)
        {
            base.Initialize(camera);
            
            _attack = new RaycastAttack(camera, _muzzle, _spreadRange, _attackMask, _damage);
        }

        public override IEnumerator PerformAttack()
        {
            _isReadyToShoot = false;
            _attack.PerformAttack();
            _view.PlayTrail(_muzzle.position, _attack.HitPosition);

            if (_attack.IsHit)
            {
                _view.PlayImpactParticles(_attack.HitPosition, _attack.HitNormal);
            }
            
            yield return new WaitForSeconds(_shotCooldownTime);
            _isReadyToShoot = true;
        }
    }
}