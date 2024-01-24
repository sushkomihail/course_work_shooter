using Extensions;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = InputSystem.PlayerInput;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _switchSpeed = 1;
        [SerializeField] private Vector3 _switchOffset = new Vector3(0, -0.2f, 0);

        private Weapon[] _playerWeapons;
        private WeaponSwitcher _switcher;
        private bool _isFireButtonPressed;

        private bool _isPaused => GameManager.Instance.PauseManager.IsPaused;

        public void Initialize(PlayerInput input, IRecoilControlAngles recoilControlAngles)
        {
            PlayerController.OnShoot.AddListener(() => _isFireButtonPressed = true);
            PlayerController.OnShootEnd.AddListener(() => _isFireButtonPressed = false);
            
            InstantiateWeapons(recoilControlAngles);
            _switcher = new WeaponSwitcher(input, _playerWeapons, _switchSpeed, _switchOffset);
        }

        public void Control(MovementStates currentMovementState, Vector2 lookInputVector)
        {
            if (_isFireButtonPressed && _switcher.CurrentWeapon.IsReadyToShoot)
            {
                StartCoroutine(_switcher.CurrentWeapon.Shoot());
                _switcher.CurrentWeapon.Recoil.SetUp();

                if (!_switcher.CurrentWeapon.CanHold)
                {
                    _isFireButtonPressed = false;
                }
            }
            
            _switcher.CurrentWeapon.Recoil.Perform();
            _switcher.CurrentWeapon.Bobbing.Perform(currentMovementState);
            _switcher.CurrentWeapon.Sway.Perform(lookInputVector);
        }
        
        public void SwitchWeapon(InputAction.CallbackContext context)
        {
            if (_switcher.IsSwitching || _isPaused) return;
            
            string key = context.control.name;

            if (key == "y")
            {
                _switcher.UpdateMouseScroll(context.ReadValue<float>());
            }
            
            StartCoroutine(_switcher.Switch(key));
        }

        private void InstantiateWeapons(IRecoilControlAngles recoilControlAngles)
        {
            Weapon[] weaponPrefabs = WeaponsProvider.SelectedWeapons;

            if (weaponPrefabs.IsEmpty()) return;

            int weaponsCount = weaponPrefabs.Length;
            _playerWeapons = new Weapon[weaponsCount];

            for (int i = 0; i < weaponsCount; i++)
            {
                Weapon weapon = Instantiate(weaponPrefabs[i], _cameraTransform);
                weapon.Initialize(_cameraTransform, recoilControlAngles);
                weapon.gameObject.SetActive(false);
                _playerWeapons[i] = weapon;
            }
        }
    }
}
