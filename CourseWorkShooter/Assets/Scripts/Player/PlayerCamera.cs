using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;

        private float _xLookAngle;
        private float _yLookAngle;
        private const int MinVerticalLookAngle = -90;
        private const int MaxVerticalLookAngle = 90;
        private float _xSensitivity = 10f;
        private float _ySensitivity = 10f;

        public void Look(Vector2 lookVector)
        {
            _xLookAngle -= lookVector.y * _xSensitivity * Time.deltaTime;
            _xLookAngle = Mathf.Clamp(_xLookAngle, MinVerticalLookAngle, MaxVerticalLookAngle);
            
            _yLookAngle += lookVector.x * _ySensitivity * Time.deltaTime;
            _yLookAngle = Mathf.Repeat(_yLookAngle, 360);
            
            transform.rotation = Quaternion.Euler(0, _yLookAngle, 0);
            _cameraTransform.localRotation = Quaternion.Euler(_xLookAngle, 0, 0);
        }
    }
}
