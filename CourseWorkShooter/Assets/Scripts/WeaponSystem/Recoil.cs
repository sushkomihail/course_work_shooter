using UnityEngine;

namespace WeaponSystem
{
    public class Recoil
    {
        private readonly Transform _weaponHolderTransform;
        private readonly Vector2 _recoilRange;
        private Vector3 _defaultRotationAngles;
        
        public Recoil(Transform weaponHolderTransform, Vector2 recoilRange)
        {
            _weaponHolderTransform = weaponHolderTransform;
            _recoilRange = recoilRange;
            _defaultRotationAngles = _weaponHolderTransform.localEulerAngles;
        }

        public void Perform()
        {
            Vector3 newRotationAngles = _weaponHolderTransform.forward +
                                  _weaponHolderTransform.right * _recoilRange.x +
                                  _weaponHolderTransform.up * _recoilRange.y;
            Quaternion newRotation = Quaternion.LookRotation(newRotationAngles);
            //_weaponHolderTransform.localRotation = Q
        }
    }
}