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
            Weapon[] weaponPrefabs = _weaponWheelConfig.FindWeapons(new[] {0});
            _weapons = new Weapon[weaponPrefabs.Length];
            
            int index = 0;
            
            foreach (Weapon prefab in weaponPrefabs)
            {
                Weapon weapon = Instantiate(prefab, _weaponHolder);
                weapon.Initialize(_camera);
                weapon.gameObject.SetActive(false);
                _weapons[index++] = weapon;
            }

            _currentWeapon = _weapons[0];
            _currentWeapon.gameObject.SetActive(true);
        }
    }
}
