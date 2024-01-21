using Extensions;
using InputSystem;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _switchSpeed = 1;
        [SerializeField] private Vector3 _switchOffset = new Vector3(0, 0.2f, 0);

        private Weapon[] _playerWeapons;
        private bool _isFireButtonPressed;

        public WeaponSwitcher Switcher { get; private set; }

        public void Initialize(PlayerInputSystem input, IRecoilControlAngles recoilControlAngles)
        {
            PlayerController.OnShoot.AddListener(() => _isFireButtonPressed = true);
            PlayerController.OnShootEnd.AddListener(() => _isFireButtonPressed = false);
            
            InstantiateWeapons(recoilControlAngles);
            Switcher = new WeaponSwitcher(input, _playerWeapons, _switchSpeed, _switchOffset);
        }

        public void Control(MovementStates currentMovementState, Vector2 lookInputVector)
        {
            if (_isFireButtonPressed && Switcher.CurrentWeapon.IsReadyToShoot)
            {
                StartCoroutine(Switcher.CurrentWeapon.Shoot());
                Switcher.CurrentWeapon.Recoil.SetUp();

                if (!Switcher.CurrentWeapon.CanHold)
                {
                    _isFireButtonPressed = false;
                }
            }
            
            Switcher.CurrentWeapon.Recoil.Perform();
            Switcher.CurrentWeapon.Bobbing.Perform(currentMovementState);
            Switcher.CurrentWeapon.Sway.Perform(lookInputVector);
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
