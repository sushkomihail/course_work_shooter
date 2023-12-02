using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private WeaponWheelConfig _weaponWheelConfig;

        private readonly List<Weapon> _weapons = new List<Weapon>();
        private Weapon _currentWeapon;
        private int _weaponsCount;
        private int _weaponId;

        private bool _isFireButtonPressed;

        private void Awake()
        {
            InstantiateWeapons();
        }

        private void Update()
        {
            PerformAttack();
        }

        public void SetWeaponId(float mouseScroll)
        {
            if (mouseScroll > 0) _weaponId += 1;
            else _weaponId -= 1;

            _weaponId %= _weaponsCount;

            if (_weaponId < 0) _weaponId += _weaponsCount;
        }
        
        public void SetFireButtonState(bool isPressed)
        {
            _isFireButtonPressed = isPressed;
        }

        public void ChangeWeapon()
        {
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon = _weapons[_weaponId];
            _currentWeapon.gameObject.SetActive(true);
        }

        private void InstantiateWeapons()
        {
            List<Weapon> weaponPrefabs = _weaponWheelConfig.FindWeapons(new[] {0, 1});

            foreach (Weapon prefab in weaponPrefabs)
            {
                Weapon weapon = Instantiate(prefab, _weaponHolder);
                weapon.Initialize(_camera);
                weapon.gameObject.SetActive(false);
                _weapons.Add(weapon);
            }

            _weaponsCount = _weapons.Count;
            _currentWeapon = _weapons[0];
            _currentWeapon.gameObject.SetActive(true);
        }
        
        private void PerformAttack()
        {
            if (_isFireButtonPressed && _currentWeapon.IsReadyToShoot)
            {
                StartCoroutine(_currentWeapon.PerformAttack());
                _currentWeapon.View.PlayMuzzleFlashParticles();
                _currentWeapon.View.PlayShootSound();

                if (!_currentWeapon.CanHold)
                {
                    _isFireButtonPressed = false;
                }
            }
        }
    }
}
