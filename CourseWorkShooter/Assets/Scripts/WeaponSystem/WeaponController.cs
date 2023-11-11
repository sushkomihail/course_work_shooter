using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private WeaponWheelConfig _weaponWheelConfig;

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

        private void Update()
        {
            
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
            }
        }

        private void InstantiateWeapons()
        {
            foreach (WeaponWheelItem item in _weaponWheelConfig.Items)
            {
                Weapon weapon = Instantiate(item.Weapon, _weaponHolder);
                weapon.SetCamera(_camera);
                weapon.gameObject.SetActive(false);
                _weapons[item.Id] = weapon;
            }

            _currentWeapon = _weapons[0];
            _currentWeapon.gameObject.SetActive(true);
        }
    }
}
