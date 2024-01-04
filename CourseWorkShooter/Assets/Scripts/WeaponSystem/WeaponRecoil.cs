using Extensions;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponRecoil : MonoBehaviour
    {
        [SerializeField] private float _kickbackSpeed = 3;
        [SerializeField] private float _kickbackReturnSpeed = 2;
        [SerializeField] private float _zOffset = 0.15f;
        [SerializeField] private Vector2 _recoilRange = new Vector2(2, 2);

        private Vector3 _weaponPositionInHolder;
        private Vector3 _targetWeaponPosition;
        private Vector3 _currentWeaponPosition;

        private ICameraAngles _cameraAngles;
        private Vector3 _targetCameraRotation;
        private float _previousEndCameraXRotation;
        
        private float _recoilSpeed;
        private float _recoilFraction;
        
        public Vector3 CurrentCameraRotation { get; private set; }

        public void Initialize(Vector3 weaponPositionInHolder, ICameraAngles cameraAngles)
        {
            _weaponPositionInHolder = weaponPositionInHolder;
            _cameraAngles = cameraAngles;
            PlayerController.OnShootEnd.AddListener(SetRecoilReturnParameters);
        }

        public void UpdateKickback()
        {
            if (_kickbackSpeed == 0) return;
            
            if (_currentWeaponPosition.IsComparableWith(_targetWeaponPosition) && _targetWeaponPosition != _weaponPositionInHolder)
            {
                _targetWeaponPosition = _weaponPositionInHolder;
                _recoilSpeed = _kickbackReturnSpeed;
            }

            _recoilFraction += _recoilSpeed * Time.deltaTime;
            CurrentCameraRotation = Vector3.Lerp(CurrentCameraRotation, _targetCameraRotation, _recoilFraction);
            _currentWeaponPosition = Vector3.Lerp(_currentWeaponPosition, _targetWeaponPosition, _recoilFraction);
            transform.localPosition = _currentWeaponPosition;
        }

        public void SetKickbackParameters()
        {
            _recoilFraction = 0;
            _recoilSpeed = _kickbackSpeed;
            _targetWeaponPosition = _weaponPositionInHolder - Vector3.forward * _zOffset;
            _targetCameraRotation += new Vector3(-_recoilRange.y, Random.Range(-_recoilRange.x, _recoilRange.x));
        }

        private void SetRecoilReturnParameters()
        {
            float endXRotation = _cameraAngles.XAngleBeforeShooting - _cameraAngles.XAngle + _previousEndCameraXRotation;
            _targetCameraRotation = Vector3.right * endXRotation;
            _previousEndCameraXRotation = endXRotation;
        }
    }
}