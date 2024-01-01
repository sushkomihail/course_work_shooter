using System.Collections;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _muzzle;
        [SerializeField] protected Vector2 _spreadRange;
        [SerializeField] protected LayerMask _attackMask;
        [SerializeField] protected int _damage;
        [SerializeField] protected float _shotCooldownTime;
        [SerializeField] private Vector3 _positionInHolder;
        [SerializeField] private bool _canHold;
        [SerializeField] private Recoil _recoil;
        [SerializeField] private WeaponView _view;

        protected Attack _attack;
        protected bool _isReadyToShoot = true;

        public WeaponView View => _view;

        public Recoil Recoil => _recoil;

        public bool IsReadyToShoot => _isReadyToShoot;
        
        public bool CanHold => _canHold;

        public virtual void Initialize(Transform cameraTransform, ICameraAngles cameraAngles)
        {
            transform.localPosition = _positionInHolder;
            _recoil.Initialize(_positionInHolder, cameraAngles);
        }

        public abstract IEnumerator PerformAttack();

        protected void Shoot()
        {
            _attack.Perform();
            _view.PlayTrail(_muzzle.position, _attack.HitPosition);

            if (_attack.IsHit)
            {
                _view.PlayImpactParticles(_attack.HitPosition, _attack.HitNormal);
            }
        }
    }
}