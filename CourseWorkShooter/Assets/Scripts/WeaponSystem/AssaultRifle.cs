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

        public override IEnumerator PerformAttack(Trail trailPrefab)
        {
            _isReadyToShoot = false;
            _attack.PerformAttack();
            Trail trail = Instantiate(trailPrefab);
            trail.StartCoroutine(trail.ShowTrail(_muzzle.position, _attack.HitPosition));
            yield return new WaitForSeconds(_shotCooldownTime);
            _isReadyToShoot = true;
        }
    }
}