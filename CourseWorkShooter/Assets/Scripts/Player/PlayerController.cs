using InputSystem;
using UnityEngine;
using WeaponSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputSystem _input;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerCamera _camera;
        [SerializeField] private WeaponController _weaponController;

        private Vector2 _currentMoveInputVector;

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
            _camera.Look(_input.Controls.Player.Look.ReadValue<Vector2>());
        }
    }
}
