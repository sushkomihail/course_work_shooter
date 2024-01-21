using Extensions;
using UnityEngine;

namespace Chest
{
    public class ChestShaker : MonoBehaviour
    {
        [SerializeField] private Transform _baseTransform;
        [SerializeField] private Transform _capTransform;
        [SerializeField] private AnimationCurve _shakeCurve;
        [SerializeField] private Vector3 _shakeRange = new Vector3(10, 10, 10);
        [SerializeField] private float _capMaxXAngle = 45;
        [SerializeField] private float _duration = 0.7f;
        [SerializeField] private float _speed = 2;
        
        private Vector3 _targetBaseAngles;
        private float _targetCapXAngle;
        private Vector3 _currentBaseAngles;
        private float _currentCapXAngle;
        private float _elapsedTime;
        
        public void Shake()
        {
            if (_currentBaseAngles.IsComparableWith(Vector3.zero) && _currentCapXAngle < 0.05f && _elapsedTime != 0)
                return;

            _elapsedTime += _speed * Time.deltaTime;
            float shakeAnglesFraction = _elapsedTime / _duration;
            
            _currentBaseAngles = _targetBaseAngles * _shakeCurve.Evaluate(shakeAnglesFraction);
            _currentCapXAngle = _targetCapXAngle * _shakeCurve.Evaluate(shakeAnglesFraction);
            
            Quaternion newBaseRotation = Quaternion.Euler(_currentBaseAngles);
            _baseTransform.localRotation =
                Quaternion.Lerp(_baseTransform.localRotation, newBaseRotation, _elapsedTime);
            
            Quaternion newCapRotation = Quaternion.Euler(_currentCapXAngle, 0, 0);
            _capTransform.localRotation =
                Quaternion.Lerp(_capTransform.localRotation, newCapRotation, _elapsedTime);
        }

        public void SetUpShakeVariables()
        {
            _elapsedTime = 0;
            
            _targetBaseAngles = new Vector3
            {
                x = Random.Range(-_shakeRange.x, _shakeRange.x),
                y = Random.Range(-_shakeRange.y, _shakeRange.y),
                z = Random.Range(-_shakeRange.z, _shakeRange.z)
            };
            _targetCapXAngle = -Random.Range(0, _capMaxXAngle);
        }
    }
}