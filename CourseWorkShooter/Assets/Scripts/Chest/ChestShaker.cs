using Extensions;
using UnityEngine;

namespace Chest
{
    public class ChestShaker : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        [SerializeField] private AnimationCurve _shakeCurve;
        [SerializeField] private float _duration;
        [SerializeField] private float _speed = 2;
        [SerializeField] private Vector3 _shakeRange = new Vector3(10, 10, 10);
        
        private Vector3 _targetAngles;
        private Vector3 _currentAngles;
        private float _elapsedTime;
        
        public void Shake()
        {
            if (_currentAngles.IsComparableWith(Vector3.zero) && _elapsedTime != 0) return;
            
            _elapsedTime += _speed * Time.deltaTime;
            float shakeAnglesFraction = _elapsedTime / _duration;
            _currentAngles = _targetAngles * _shakeCurve.Evaluate(shakeAnglesFraction);
            Quaternion newRotation = Quaternion.Euler(_currentAngles);
            _model.localRotation = Quaternion.Lerp(_model.localRotation, newRotation, _elapsedTime);
        }

        public void SetUpShakeVariables()
        {
            _elapsedTime = 0;
            _targetAngles = new Vector3
            {
                x = Random.Range(-_shakeRange.x, _shakeRange.x),
                y = Random.Range(-_shakeRange.y, _shakeRange.y),
                z = Random.Range(-_shakeRange.z, _shakeRange.z)
            };
        }
    }
}