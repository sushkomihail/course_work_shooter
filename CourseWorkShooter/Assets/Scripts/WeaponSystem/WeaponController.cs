using System.Collections.Generic;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private WeaponsCollection weaponsCollection;

        private readonly List<Weapon> _playerWeapons = new List<Weapon>();
        private Weapon _currentWeapon;
        private int _weaponId;

        private bool _isFireButtonPressed;

        public void Initialize(IRecoilControlAngles recoilControlAngles)
        {
            PlayerController.OnShoot.AddListener(() => _isFireButtonPressed = true);
            PlayerController.OnShootEnd.AddListener(() => _isFireButtonPressed = false);
            InstantiateWeapons(recoilControlAngles);
        }
        
        public void UpdateWeapon(MovementStates currentMovementState, Vector2 lookInputVector)
        {
            if (_isFireButtonPressed && _currentWeapon.IsReadyToShoot)
            {
                StartCoroutine(_currentWeapon.PerformAttack());
                _currentWeapon.Recoil.SetUp();
                _currentWeapon.View.PlayMuzzleFlashParticles();
                _currentWeapon.View.PlayShootSound();

                if (!_currentWeapon.CanHold)
                {
                    _isFireButtonPressed = false;
                }
            }
            
            _currentWeapon.Recoil.UpdateKickback();
            _currentWeapon.Bobbing.Perform(currentMovementState);
            _currentWeapon.Sway.Perform(lookInputVector);
        }

        public void SetWeaponId(float mouseScroll)
        {
            if (mouseScroll > 0) _weaponId += 1;
            else _weaponId -= 1;

            _weaponId %= _playerWeapons.Count;

            if (_weaponId < 0) _weaponId += _playerWeapons.Count;
        }

        public void ChangeWeapon()
        {
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon = _playerWeapons[_weaponId];
            _currentWeapon.gameObject.SetActive(true);
        }

        private void InstantiateWeapons(IRecoilControlAngles recoilControlAngles)
        {
            if (weaponsCollection == null) return;
            
            Weapon[] weaponPrefabs = weaponsCollection.Weapons;

            foreach (Weapon prefab in weaponPrefabs)
            {
                Weapon weapon = Instantiate(prefab, _cameraTransform);
                weapon.Initialize(_cameraTransform, recoilControlAngles);
                weapon.gameObject.SetActive(false);
                _playerWeapons.Add(weapon);
            }

            _currentWeapon = _playerWeapons[0];
            _currentWeapon.gameObject.SetActive(true);
        }
    }
}
