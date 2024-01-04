using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _acceleration = 2;
        [SerializeField] private float _walkSpeed = 6;
        [SerializeField] private float _runSpeed = 12;
        [SerializeField] private float _jumpHeight = 7;
        [SerializeField] private float _jumpTime = 1;
        [SerializeField] private float _defaultHeight = 2;
        [SerializeField] private float _crouchHeight = 1;
        [SerializeField] private float _crouchSpeed = 3;

        private float _gravity;
        private Vector2 _currentMoveInputVector;
        private Vector3 _lastMoveDirection;
        private Vector3 _currentMoveDirection;
        private float _currentSpeed;
        private bool _isRunning;
        private float _jumpVelocity;
        private float _targetHeight;
        private float _crouchLerpFraction;
        private bool _isCrouching;
        
        public MovementStates CurrentState { get; private set; }

        public void Initialize()
        {
            float timeToApex = _jumpTime / 2;
            _gravity = 2 * _jumpHeight / Mathf.Pow(timeToApex, 2);
            _jumpVelocity = 2 * _jumpHeight / timeToApex;
            _targetHeight = _defaultHeight;
        }
        
        public void HandleState(MovementStates state)
        {
            CurrentState = state;
            
            switch (CurrentState)
            {
                case MovementStates.Walk:
                    OnWalk();
                    break;
                case MovementStates.Run:
                    OnRun();
                    break;
                case MovementStates.Jump:
                    OnJump();
                    break;
                case MovementStates.Crouch:
                    OnCrouch();
                    break;
            }
        }

        public void Move(Vector2 targetInputVector)
        {
            _currentMoveInputVector = 
                Vector2.MoveTowards(_currentMoveInputVector, targetInputVector, _acceleration * Time.deltaTime);
            Vector3 inputVectorRelativeToPlayer = 
                transform.forward * _currentMoveInputVector.y + transform.right * _currentMoveInputVector.x;
            _currentMoveDirection.x = inputVectorRelativeToPlayer.x * _currentSpeed;
            _currentMoveDirection.z = inputVectorRelativeToPlayer.z * _currentSpeed;
            
            if (!_controller.isGrounded)
            {
                Vector3 targetMoveDirection = _currentMoveDirection;
                _currentMoveDirection = _lastMoveDirection;
                _currentMoveDirection = 
                    Vector3.Lerp(_currentMoveDirection, targetMoveDirection, _acceleration * Time.deltaTime);
                _currentMoveDirection.y -= _gravity * Time.deltaTime;
            }

            _lastMoveDirection = _currentMoveDirection;
            _controller.Move(_currentMoveDirection * Time.deltaTime);
            Crouch();
        }

        private void OnWalk()
        {
            _currentSpeed = _walkSpeed;
        }
        
        private void OnRun()
        {
            _targetHeight = _defaultHeight;
            _currentSpeed = _runSpeed;
        }

        private void OnJump()
        {
            if (!_controller.isGrounded) return;

            _targetHeight = _defaultHeight;
            _currentMoveDirection.y = _jumpVelocity;
        }

        private void OnCrouch()
        {
            _crouchLerpFraction = 0;

            if (_isCrouching)
            {
                _targetHeight = _defaultHeight;
                _currentSpeed = _walkSpeed;
                _isCrouching = false;
            }
            else
            {
                _targetHeight = _crouchHeight;
                _currentSpeed = _crouchSpeed;
                _isCrouching = true;
            }
        }

        private void Crouch()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (_controller.height == _targetHeight) return;
            
            _crouchLerpFraction += _crouchSpeed * Time.deltaTime;
            float lastHeight = _controller.height;
            _controller.height = Mathf.Lerp(_controller.height, _targetHeight, _crouchLerpFraction);

            if (Mathf.Abs(_controller.height - _targetHeight) < 0.05f)
            {
                _controller.height = _targetHeight;
            }

            if (_targetHeight - _defaultHeight < 0 && _controller.isGrounded)
            {
                _currentMoveDirection.y -= _gravity * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.up * (_controller.height - lastHeight) / 2;
            }
        }
    }
}