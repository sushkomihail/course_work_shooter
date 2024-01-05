using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponBobbing : MonoBehaviour
    {
        [SerializeField] private Vector2 _scale = new Vector2(0.01f, 0.01f);
        [SerializeField] private float _onIdleSpeed = 2;
        [SerializeField] private float _onWalkSpeed = 6;
        [SerializeField] private float _onRunSpeed = 8;

        private float _elapsedTime;
        private float _currentSpeed;
        private Vector2 _offset;

        public void Perform(MovementStates currentMovementState)
        {
            if (_scale == Vector2.zero) return;
            
            switch (currentMovementState)
            {
                case MovementStates.Walk:
                    _currentSpeed = _onWalkSpeed;
                    break;
                case MovementStates.Run:
                    _currentSpeed = _onRunSpeed;
                    break;
                default:
                    _currentSpeed = _onIdleSpeed;
                    break;
            }

            _elapsedTime += _currentSpeed * Time.deltaTime;
            
            _offset.x = _scale.x * Mathf.Cos(_elapsedTime);
            _offset.y = _scale.y * Mathf.Sin(_elapsedTime * 2);
            Vector3 targetPosition = transform.localPosition + new Vector3(_offset.x, _offset.y);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, _elapsedTime);
        }
    }
}