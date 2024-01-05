using InputSystem;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputSystem _input;
        [SerializeField] private PlayerCamera _camera;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private WeaponController _weaponController;

        public static readonly UnityEvent OnShoot = new UnityEvent();
        public static readonly UnityEvent OnShootEnd = new UnityEvent();
        
        private Vector2 _currentMoveInputVector;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _input.Initialize();
            _camera.Initialize();
            _movement.Initialize();
            _weaponController.Initialize(_camera);

            _movement.HandleState(MovementStates.Idle);
            
            _input.Controls.Player.Move.started += _ => _movement.HandleState(MovementStates.Walk);
            _input.Controls.Player.Move.canceled += _ => _movement.HandleState(MovementStates.Idle);
            _input.Controls.Player.Run.started += _ => _movement.HandleState(MovementStates.Run);
            _input.Controls.Player.Run.canceled += _ => _movement.HandleState(MovementStates.Walk);
            _input.Controls.Player.Jump.performed += _ => _movement.HandleState(MovementStates.Jump);
            _input.Controls.Player.Crouch.performed += _ => _movement.HandleState(MovementStates.Crouch);

            _input.Controls.Player.ChangeWeapon.started += context =>
            {
                _weaponController.SetWeaponId(context.ReadValue<float>());
                _weaponController.ChangeWeapon();
            };
            _input.Controls.Player.Shoot.started += _ => OnShoot?.Invoke();
            _input.Controls.Player.Shoot.canceled += _ => OnShootEnd?.Invoke();
        }

        private void OnEnable()
        {
            _input.OnActivate();
        }

        private void OnDisable()
        {
            _input.OnDisactivate();
        }

        private void Update()
        {
            Vector2 lookInputVector = _input.Controls.Player.Look.ReadValue<Vector2>();
            Vector2 moveInputVector = _input.Controls.Player.Move.ReadValue<Vector2>();
            
            _camera.Look(lookInputVector);
            _movement.Move(moveInputVector);
            _weaponController.UpdateWeapon(_movement.CurrentState, lookInputVector);
        }
    }
}
