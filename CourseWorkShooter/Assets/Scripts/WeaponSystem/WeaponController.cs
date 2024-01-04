using System.Collections.Generic;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private WeaponWheelConfig _weaponWheelConfig;

        private readonly List<Weapon> _weapons = new List<Weapon>();
        private int _weaponId;

        private bool _isFireButtonPressed;
        
        public Weapon CurrentWeapon { get; private set; }

        public void Initialize(ICameraAngles cameraAngles)
        {
            PlayerController.OnShoot.AddListener(() => _isFireButtonPressed = true);
            PlayerController.OnShootEnd.AddListener(() => _isFireButtonPressed = false);
            InstantiateWeapons(cameraAngles);
        }
        
        public void UpdateWeapon(MovementStates currentMovementState, Vector2 lookInputVector)
        {
            if (_isFireButtonPressed && CurrentWeapon.IsReadyToShoot)
            {
                StartCoroutine(CurrentWeapon.PerformAttack());
                CurrentWeapon.Recoil.SetKickbackParameters();
                CurrentWeapon.View.PlayMuzzleFlashParticles();
                CurrentWeapon.View.PlayShootSound();

                if (!CurrentWeapon.CanHold)
                {
                    _isFireButtonPressed = false;
                }
            }
            
            CurrentWeapon.Recoil.UpdateKickback();
            CurrentWeapon.Bobbing.Perform(currentMovementState);
            CurrentWeapon.Sway.Perform(lookInputVector);
        }

        public void SetWeaponId(float mouseScroll)
        {
            if (mouseScroll > 0) _weaponId += 1;
            else _weaponId -= 1;

            _weaponId %= _weapons.Count;

            if (_weaponId < 0) _weaponId += _weapons.Count;
        }

        public void ChangeWeapon()
        {
            CurrentWeapon.gameObject.SetActive(false);
            CurrentWeapon = _weapons[_weaponId];
            CurrentWeapon.gameObject.SetActive(true);
        }

        private void InstantiateWeapons(ICameraAngles cameraAngles)
        {
            if (_weaponWheelConfig == null) return;
            
            Weapon[] weaponPrefabs = _weaponWheelConfig.Weapons;

            foreach (Weapon prefab in weaponPrefabs)
            {
                Weapon weapon = Instantiate(prefab, _cameraTransform);
                weapon.Initialize(_cameraTransform, cameraAngles);
                weapon.gameObject.SetActive(false);
                _weapons.Add(weapon);
            }

            CurrentWeapon = _weapons[0];
            CurrentWeapon.gameObject.SetActive(true);
        }
    }
}
