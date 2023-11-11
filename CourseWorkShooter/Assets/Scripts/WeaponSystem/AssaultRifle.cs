using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class AssaultRifle : Weapon
    {
        [SerializeField] private int _fireRate;

        private Attack _attack;
        private float _timeBetweenShots;
        
        private void Awake()
        {
            _attack = new RaycastAttack(_camera, _muzzle, _spreadRange, _attackMask, _damage);
            _timeBetweenShots = 60.0f / _fireRate;
        }

        public override IEnumerator PerformAttack()
        {
            _canAttack = false;
            Debug.Log("shot!");
            yield return new WaitForSeconds(_timeBetweenShots);
            _canAttack = true;
        }
    }
}