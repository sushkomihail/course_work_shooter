using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _walkSpeed = 6f;
        [SerializeField] private float _runSpeed = 12f;
        [SerializeField] private float _jumpPower = 7f;
        [SerializeField] private float _defaultHeight = 2f;
        [SerializeField] private float _crouchHeight = 1f;
        [SerializeField] private float _crouchSpeed = 3f;
        
        private float _gravity;
        private Vector3 _moveDirection;
        private float _speed;
        private bool _isCrouching;

        void Start()
        {
            _gravity = Physics.gravity.magnitude;

            _controller.height = _defaultHeight;
            _speed = _walkSpeed;
        }

        public void Move(Vector2 inputVector)
        {
            Vector3 inputVectorRelativeToPlayer = transform.forward * inputVector.y + transform.right * inputVector.x;
            _moveDirection.x = inputVectorRelativeToPlayer.x * _speed;
            _moveDirection.z = inputVectorRelativeToPlayer.z * _speed;
            
            if (!_controller.isGrounded)
            {
                _moveDirection.y -= _gravity * Time.deltaTime;
            }

            _controller.Move(_moveDirection * Time.deltaTime);
        }

        public void OnRun(bool isRunButtonPressed)
        {
            if (_isCrouching) return;

            if (isRunButtonPressed)
            {
                _speed = _runSpeed;
            }
            else
            {
                _speed = _walkSpeed;
            }
        }

        public void OnJump()
        {
            if (!_controller.isGrounded) return;
            
            _moveDirection.y = _jumpPower;
            Debug.Log("jump");
        }

        public void OnCrouch(bool isCrouchButtonPressed)
        {
            if (isCrouchButtonPressed)
            {
                _isCrouching = true;
                _controller.height = _crouchHeight;
                _speed = _crouchSpeed;
            }
            else
            {
                _controller.height = _defaultHeight;
                _speed = _walkSpeed;
                _isCrouching = false;
            }
        }
    }
}