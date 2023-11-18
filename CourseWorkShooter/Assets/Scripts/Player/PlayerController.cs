using InputSystem;
using UnityEngine;
using WeaponSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputSystem _input;
        [SerializeField] private WeaponController _weaponController;
        [SerializeField] private Transform _cameraTransform;

        private float _xLookAngle;
        private float _yLookAngle;
        private const int MinVerticalLookAngle = -90;
        private const int MaxVerticalLookAngle = 90;
        private float _xSensitivity = 10f;
        private float _ySensitivity = 10f;

        private void Awake()
        {
            _input.Initialize();
            _input.Controls.Player.ChangeWeapon.performed += context =>
            {
                _weaponController.SetWeaponId(context.ReadValue<float>());
                _weaponController.ChangeWeapon();
            };
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Look();

            if (_input.Controls.Player.Shoot.IsPressed())
            {
                _weaponController.PerformAttack();
            }
        }

        private void Look()
        {
            Vector2 lookVector = _input.Controls.Player.Look.ReadValue<Vector2>();
            
            _xLookAngle -= lookVector.y * _xSensitivity * Time.deltaTime;
            _xLookAngle = Mathf.Clamp(_xLookAngle, MinVerticalLookAngle, MaxVerticalLookAngle);
            
            _yLookAngle += lookVector.x * _ySensitivity * Time.deltaTime;
            _yLookAngle = Mathf.Repeat(_yLookAngle, 360);
            
            transform.rotation = Quaternion.Euler(0, _yLookAngle, 0);
            _cameraTransform.localRotation = Quaternion.Euler(_xLookAngle, 0, 0);
        }
    }
}
