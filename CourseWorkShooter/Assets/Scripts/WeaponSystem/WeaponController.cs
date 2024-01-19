using InputSystem;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private SelectedWeaponsCollection _selectedWeapons;
        [SerializeField] private float _switchSpeed = 1;
        [SerializeField] private Vector3 _switchOffset = new Vector3(0, 0.2f, 0);

        private Weapon[] _playerWeapons;
        private int _weaponId;

        private bool _isFireButtonPressed;
        
        public WeaponSwitcher Switcher;

        public void Initialize(PlayerInputSystem input, IRecoilControlAngles recoilControlAngles)
        {
            PlayerController.OnShoot.AddListener(() => _isFireButtonPressed = true);
            PlayerController.OnShootEnd.AddListener(() => _isFireButtonPressed = false);
            
            InstantiateWeapons(recoilControlAngles);
            Switcher = new WeaponSwitcher(input, _playerWeapons, _switchSpeed, _switchOffset);
        }
        
        public void UpdateWeapon(MovementStates currentMovementState, Vector2 lookInputVector)
        {
            if (_isFireButtonPressed && Switcher.CurrentWeapon.IsReadyToShoot)
            {
                StartCoroutine(Switcher.CurrentWeapon.PerformAttack());
                Switcher.CurrentWeapon.Recoil.SetUp();
                Switcher.CurrentWeapon.View.PlayMuzzleFlashParticles();
                Switcher.CurrentWeapon.View.PlayShootSound();

                if (!Switcher.CurrentWeapon.CanHold)
                {
                    _isFireButtonPressed = false;
                }
            }
            
            Switcher.CurrentWeapon.Recoil.UpdateKickback();
            Switcher.CurrentWeapon.Bobbing.Perform(currentMovementState);
            Switcher.CurrentWeapon.Sway.Perform(lookInputVector);
        }

        private void InstantiateWeapons(IRecoilControlAngles recoilControlAngles)
        {
            if (_selectedWeapons == null) return;
            
            Weapon[] weaponPrefabs = _selectedWeapons.WeaponPrefabs;
            _playerWeapons = new Weapon[weaponPrefabs.Length];

            for (int i = 0; i < weaponPrefabs.Length; i++)
            {
                Weapon weapon = Instantiate(weaponPrefabs[i], _cameraTransform);
                weapon.Initialize(_cameraTransform, recoilControlAngles);
                weapon.gameObject.SetActive(false);
                _playerWeapons[i] = weapon;
            }
        }
    }
}
