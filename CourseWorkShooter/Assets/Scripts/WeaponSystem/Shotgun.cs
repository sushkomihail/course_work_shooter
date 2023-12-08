using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class Shotgun : Weapon
    {
        [SerializeField] private int _shotsCount;
        
        public override void Initialize(Camera camera)
        {
            base.Initialize(camera);
            
            _attack = new RaycastAttack(camera, _muzzle, _spreadRange, _attackMask, _damage);
        }

        public override IEnumerator PerformAttack()
        {
            _isReadyToShoot = false;
            
            for (int i = 0; i < _shotsCount; i++)
            {
                _attack.Perform();
                _view.PlayTrail(_muzzle.position, _attack.HitPosition);

                if (_attack.IsHit)
                {
                    _view.PlayImpactParticles(_attack.HitPosition, _attack.HitNormal);
                }
            }
            
            yield return new WaitForSeconds(_shotCooldownTime);
            _isReadyToShoot = true;
        }
    }
}