using InputSystem;
using UnityEngine;
using WeaponSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputSystem _input;
        [SerializeField] private PlayerMovement _movement;
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

            _input.Controls.Player.Run.started += _ => _movement.OnRun(true);
            _input.Controls.Player.Run.canceled += _ => _movement.OnRun(false);
            _input.Controls.Player.Jump.performed += _ => _movement.OnJump();
            _input.Controls.Player.Crouch.started += _ => _movement.OnCrouch(true);
            _input.Controls.Player.Crouch.canceled += _ => _movement.OnCrouch(false);
            
            _input.Controls.Player.ChangeWeapon.performed += context =>
            {
                _weaponController.SetWeaponId(context.ReadValue<float>());
                _weaponController.ChangeWeapon();
            };
            _input.Controls.Player.Shoot.started += _ => _weaponController.SetFireButtonState(true);
            _input.Controls.Player.Shoot.canceled += _ => _weaponController.SetFireButtonState(false);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            _movement.Move(_input.Controls.Player.Move.ReadValue<Vector2>());
            Look();
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
