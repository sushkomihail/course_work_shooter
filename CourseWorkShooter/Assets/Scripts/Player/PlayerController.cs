using HealthSystem;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;
using PlayerInput = InputSystem.PlayerInput;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerCamera _camera;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerWeaponController _weaponController;
        [SerializeField] private PlayerHealth _health;

        public static readonly UnityEvent OnShoot = new UnityEvent();
        public static readonly UnityEvent OnShootEnd = new UnityEvent();
        
        public PlayerCamera Camera => _camera;
        private bool _isPaused => GameManager.Instance.PauseManager.IsPaused;

        private void Awake()
        {
            _input.Initialize();
            _camera.Initialize();
            _movement.Initialize();
            _weaponController.Initialize(_input, _camera);
            _health.Initialize();

            _movement.HandleState(MovementStates.Idle);
            
            _input.Controls.Player.Move.started += _ => _movement.HandleState(MovementStates.Walk);
            _input.Controls.Player.Move.canceled += _ => _movement.HandleState(MovementStates.Idle);
            _input.Controls.Player.Run.started += _ => _movement.HandleState(MovementStates.Run);
            _input.Controls.Player.Run.canceled += _ => _movement.HandleState(MovementStates.Walk);
            _input.Controls.Player.Jump.performed += _ => _movement.HandleState(MovementStates.Jump);
            _input.Controls.Player.Crouch.performed += _ => _movement.HandleState(MovementStates.Crouch);

            _input.Controls.Player.SwitchWeapon.performed += _weaponController.SwitchWeapon;
            _input.Controls.Player.Shoot.started += _ => OnShoot?.Invoke();
            _input.Controls.Player.Shoot.canceled += _ => OnShootEnd?.Invoke();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            if (_isPaused) return;
            
            Vector2 lookInputVector = _input.Controls.Player.Look.ReadValue<Vector2>();
            Vector2 moveInputVector = _input.Controls.Player.Move.ReadValue<Vector2>();
            
            _camera.Look(lookInputVector);
            _movement.Move(moveInputVector);
            _weaponController.Control(_movement.CurrentState, lookInputVector);
        }
    }
}
