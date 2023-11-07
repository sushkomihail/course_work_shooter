using InputSystem;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputSystem _input;
        [SerializeField] private Camera _camera;

        private float _xLookAngle;
        private float _yLookAngle;
        private const int MinVerticalLookAngle = -90;
        private const int MaxVerticalLookAngle = 90;
        private float _xSensitivity = 7.5f;
        private float _ySensitivity = 7.5f;

        private void Update()
        {
            Look();
        }

        private void Look()
        {
            Vector2 lookVector = _input.Controls.Movement.Look.ReadValue<Vector2>();
            
            _xLookAngle -= lookVector.y * _xSensitivity * Time.deltaTime;
            _xLookAngle = Mathf.Clamp(_xLookAngle, MinVerticalLookAngle, MaxVerticalLookAngle);
            
            _yLookAngle += lookVector.x * _ySensitivity * Time.deltaTime;
            _yLookAngle = Mathf.Repeat(_yLookAngle, 360);
            
            transform.rotation = Quaternion.Euler(0, _yLookAngle, 0);
            _camera.transform.localRotation = Quaternion.Euler(_xLookAngle, 0, 0);
        }
    }
}
