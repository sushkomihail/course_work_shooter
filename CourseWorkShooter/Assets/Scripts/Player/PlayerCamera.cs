using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour, ICameraAngles
    {
        [SerializeField] private Transform _cameraHolder;

        private float _xAngle;
        private float _yAngle;
        private const int MinXAngle = -90;
        private const int MaxXAngle = 90;
        private float _xSensitivity = 10f;
        private float _ySensitivity = 10f;
        private float _xAngleBeforeShooting;

        public float XAngle => _xAngle;

        public float XAngleBeforeShooting => _xAngleBeforeShooting;

        public void Initialize()
        {
            PlayerController.OnShoot.AddListener(() => _xAngleBeforeShooting = _xAngle);
        }

        public void Look(Vector2 lookVector, Vector3 recoilRotation)
        {
            _xAngle -= lookVector.y * _xSensitivity * Time.deltaTime;
            _xAngle = Mathf.Clamp(_xAngle,
                MinXAngle - recoilRotation.x,
                MaxXAngle - recoilRotation.x);
        
            _yAngle += lookVector.x * _ySensitivity * Time.deltaTime;
            _yAngle = Mathf.Repeat(_yAngle, 360);
            
            transform.rotation = Quaternion.Euler(0, _yAngle, 0);
            _cameraHolder.localRotation = Quaternion.Euler(_xAngle + recoilRotation.x, recoilRotation.y, 0);
        }
    }
}
