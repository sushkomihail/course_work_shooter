using InputSystem;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private PlayerInputSystem _input;
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

        private void Start()
        {
            _input.Controls.Player.ChangeWeapon.performed += context =>
            {
                SetWeaponId(context.ReadValue<float>());
                ChangeWeapon();
            };
        }

        private void Update()
        {
            
        }

        private void InstantiateWeapons()
        {
            foreach (WeaponWheelItem item in _weaponWheelConfig.Items)
            {
                Weapon weapon = Instantiate(item.Weapon, _weaponHolder);
                weapon.transform.localPosition = item.Weapon.PositionInHolder;
                weapon.transform.localRotation = Quaternion.identity;
                
                _weapons[item.Id] = weapon;
                weapon.gameObject.SetActive(false);
            }

            _currentWeapon = _weapons[0];
            _currentWeapon.gameObject.SetActive(true);
        }

        private void SetWeaponId(float mouseScroll)
        {
            if (mouseScroll > 0) _weaponId += 1;
            else _weaponId -= 1;

            _weaponId %= _weaponsCount;

            if (_weaponId < 0) _weaponId += _weaponsCount;
        }

        private void ChangeWeapon()
        {
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon = _weapons[_weaponId];
            _currentWeapon.gameObject.SetActive(true);
        }
    }
}
