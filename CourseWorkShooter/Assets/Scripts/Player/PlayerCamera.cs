using System;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour, ICameraAngles
    {
        [SerializeField] private Transform _cameraHolder;

        private float _yAngle;
        private const int MinXAngle = -90;
        private const int MaxXAngle = 90;
        private float _xSensitivity = 10;
        private float _ySensitivity = 10;
        private Vector3 _recoilAngles;

        public float XAngle { get; private set; }
        public float XAngleBeforeShooting { get; private set; }
        public Vector3 RecoilAngles => _recoilAngles;

        public void Initialize()
        {
            PlayerController.OnShoot.AddListener(() => XAngleBeforeShooting = XAngle);
        }

        public void Look(Vector2 lookInputVector)
        {
            XAngle -= lookInputVector.y * _xSensitivity * Time.deltaTime;
            XAngle = Mathf.Clamp(XAngle,
                MinXAngle - _recoilAngles.x,
                MaxXAngle - _recoilAngles.x);
        
            _yAngle += lookInputVector.x * _ySensitivity * Time.deltaTime;
            _yAngle = Mathf.Repeat(_yAngle, 360);
            
            transform.rotation = Quaternion.Euler(0, _yAngle, 0);
            _cameraHolder.localRotation = Quaternion.Euler(XAngle + _recoilAngles.x, _recoilAngles.y, 0);
        }

        public void SetRecoilAngles(Vector3 recoilAngles)
        {
            _recoilAngles = recoilAngles;
        }
    }
}
