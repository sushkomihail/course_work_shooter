using Extensions;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponRecoil : MonoBehaviour
    {
        [SerializeField] private float _kickbackSpeed = 3;
        [SerializeField] private float _kickbackReturnSpeed = 2;
        [SerializeField] private float _recoilSpeed = 3;
        [SerializeField] private float _recoilReturnSpeed = 0.7f;
        [SerializeField] private float _zOffset = 0.15f;
        [SerializeField] private Vector2 _recoilRange = new Vector2(2, 2);

        private Vector3 _weaponPositionInHolder;
        private Vector3 _targetWeaponPosition;
        private Vector3 _currentWeaponPosition;

        private IRecoilControlAngles _recoilControlAngles;
        private Vector3 _targetCameraRotation;
        private float _previousEndCameraXRotation;

        private float _currentKickbackSpeed;
        private float _currentRecoilSpeed;
        private float _kickbackFraction;
        private float _recoilFraction;

        public void Initialize(Vector3 weaponPositionInHolder, IRecoilControlAngles recoilControlAngles)
        {
            _weaponPositionInHolder = weaponPositionInHolder;
            _currentWeaponPosition = _weaponPositionInHolder;
            _recoilControlAngles = recoilControlAngles;
            PlayerController.OnShootEnd.AddListener(SetRecoilReturnParameters);
        }

        public void Perform()
        {
            if (_currentWeaponPosition.IsComparableWith(_targetWeaponPosition) && _targetWeaponPosition != _weaponPositionInHolder)
            {
                _targetWeaponPosition = _weaponPositionInHolder;
                _currentKickbackSpeed = _kickbackReturnSpeed;
            }

            _kickbackFraction += _currentKickbackSpeed * Time.deltaTime;
            _currentWeaponPosition = Vector3.Lerp(_currentWeaponPosition, _targetWeaponPosition, _kickbackFraction);
            transform.localPosition = _currentWeaponPosition;

            _recoilFraction += _currentRecoilSpeed * Time.deltaTime;
            Vector3 recoilAngles = _recoilControlAngles.RecoilAngles;
            recoilAngles = Vector3.Lerp(recoilAngles, _targetCameraRotation, _recoilFraction);
            _recoilControlAngles.SetRecoilAngles(recoilAngles);
        }

        public void SetUp()
        {
            _kickbackFraction = 0;
            _recoilFraction = 0;
            _currentKickbackSpeed = _kickbackSpeed;
            _currentRecoilSpeed = _recoilSpeed;
            _targetWeaponPosition = _weaponPositionInHolder - Vector3.forward * _zOffset;
            _targetCameraRotation += new Vector3(-_recoilRange.y, Random.Range(-_recoilRange.x, _recoilRange.x));
        }

        private void SetRecoilReturnParameters()
        {
            _recoilFraction = 0;
            _currentRecoilSpeed = _recoilReturnSpeed;
            float endXRotation = _recoilControlAngles.XAngleBeforeShooting - _recoilControlAngles.XAngle + _previousEndCameraXRotation;
            _targetCameraRotation = Vector3.right * endXRotation;
            _previousEndCameraXRotation = endXRotation;
        }
    }
}