using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private WeaponWheelConfig _weaponWheelConfig;
        [SerializeField] private Trail _trailPrefab;

        private Weapon[] _weapons;
        private Weapon _currentWeapon;
        private int _weaponsCount;
        private int _weaponId;

        private void Awake()
        {
            _weaponsCount = _weaponWheelConfig.Items.Length;
            _weapons = new Weapon[_weaponsCount];
            InstantiateWeapons();
        }
        
        public void SetWeaponId(float mouseScroll)
        {
            if (mouseScroll > 0) _weaponId += 1;
            else _weaponId -= 1;

            _weaponId %= _weaponsCount;

            if (_weaponId < 0) _weaponId += _weaponsCount;
        }

        public void ChangeWeapon()
        {
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon = _weapons[_weaponId];
            _currentWeapon.gameObject.SetActive(true);
        }
        
        public void PerformAttack()
        {
            if (_currentWeapon.CanAttack)
            {
                StartCoroutine(_currentWeapon.PerformAttack());

                if (_currentWeapon.UseTrail)
                {
                    Trail trail = Instantiate(_trailPrefab, _currentWeapon.Muzzle.position, Quaternion.identity);
                    StartCoroutine(trail.ShowTrail(_currentWeapon.Muzzle.position, _currentWeapon.Attack.HitPosition));
                }
            }
        }

        private void InstantiateWeapons()
        {
            foreach (WeaponWheelItem item in _weaponWheelConfig.Items)
            {
                Weapon weapon = Instantiate(item.Weapon, _weaponHolder);
                weapon.Initialize(_camera);
                weapon.gameObject.SetActive(false);
                _weapons[item.Id] = weapon;
            }

            _currentWeapon = _weapons[0];
            _currentWeapon.gameObject.SetActive(true);
        }
    }
}
