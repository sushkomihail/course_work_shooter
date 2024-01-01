using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _walkSpeed = 6f;
        [SerializeField] private float _runSpeed = 12f;
        [SerializeField] private float _jumpHeight = 7f;
        [SerializeField] private float _jumpTime = 1f;
        [SerializeField] private float _defaultHeight = 2f;
        [SerializeField] private float _crouchHeight = 1f;
        [SerializeField] private float _crouchSpeed = 3f;
        [SerializeField] private float _crouchTime = 0.5f;
        [SerializeField, Range(0, 1)] private float _smoothingRate = 0.25f;
        
        private float _gravity;
        private Vector2 _currentMoveInputVector;
        private Vector3 _lastMoveDirection;
        private Vector3 _currentMoveDirection;
        private float _currentSpeed;
        private float _jumpVelocity;
        private IEnumerator _currentCrouchRoutine;
        private bool _isCrouching;

        public void Initialize()
        {
            _controller.height = _defaultHeight;
            _currentSpeed = _walkSpeed;
            SetUpJumpVariables();
        }

        public void Move(Vector2 targetInputVector)
        {
            if (!_controller.isGrounded)
            {
                _currentMoveDirection = _lastMoveDirection;
                _currentMoveDirection.y -= _gravity * Time.deltaTime;
            }
            else
            {
                _currentMoveInputVector = Vector2.MoveTowards(_currentMoveInputVector, targetInputVector,
                    Time.deltaTime / _smoothingRate);
                Vector3 inputVectorRelativeToPlayer = 
                    transform.forward * _currentMoveInputVector.y + transform.right * _currentMoveInputVector.x;
                _currentMoveDirection.x = inputVectorRelativeToPlayer.x * _currentSpeed;
                _currentMoveDirection.z = inputVectorRelativeToPlayer.z * _currentSpeed;
            }

            _lastMoveDirection = _currentMoveDirection;
            _controller.Move(_currentMoveDirection * Time.deltaTime);
        }

        public void OnRun(bool isRunButtonPressed)
        {
            if (_isCrouching) return;

            if (isRunButtonPressed)
            {
                _currentSpeed = _runSpeed;
            }
            else
            {
                _currentSpeed = _walkSpeed;
            }
        }

        public void OnJump()
        {
            if (!_controller.isGrounded) return;
            
            _currentMoveDirection.y = _jumpVelocity;
        }

        public void OnCrouch(bool isCrouchButtonPressed)
        {
            if (isCrouchButtonPressed)
            {
                _isCrouching = true;
                StartCrouchSmoothing(_crouchHeight);
                _currentSpeed = _crouchSpeed;
            }
            else
            {
                StartCrouchSmoothing(_defaultHeight);
                _currentSpeed = _walkSpeed;
                _isCrouching = false;
            }
        }

        private void SetUpJumpVariables()
        {
            float timeToApex = _jumpTime / 2;
            _gravity = 2 * _jumpHeight / Mathf.Pow(timeToApex, 2);
            _jumpVelocity = 2 * _jumpHeight / timeToApex;
        }

        private IEnumerator SmoothCrouchRoutine(float targetHeight)
        {
            float currentHeight = _controller.height;
            float elapsedTime = 0;

            while (elapsedTime < _crouchTime)
            {
                currentHeight = Mathf.Lerp(currentHeight, targetHeight, elapsedTime / _crouchTime);
                _controller.height = currentHeight;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _controller.height = targetHeight;
        }

        private void StartCrouchSmoothing(float targetHeight)
        {
            if (_currentCrouchRoutine != null)
            {
                StopCoroutine(_currentCrouchRoutine);
            }

            _currentCrouchRoutine = SmoothCrouchRoutine(targetHeight);
            StartCoroutine(_currentCrouchRoutine);
        }
    }
}