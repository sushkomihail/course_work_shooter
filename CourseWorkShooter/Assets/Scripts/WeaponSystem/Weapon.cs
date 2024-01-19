using System.Collections;
using AttackSystem;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _muzzle;
        [SerializeField] protected Vector2 _spreadRange = new Vector2(0.02f, 0.02f);
        [SerializeField] protected LayerMask _attackMask;
        [SerializeField] protected int _damage = 20;
        [SerializeField] protected float _shotCooldownTime = 0.2f;
        [SerializeField] private Vector3 _positionInHolder;
        [SerializeField] private bool _canHold;
        [SerializeField] private WeaponRecoil _recoil;
        [SerializeField] private WeaponBobbing _bobbing;
        [SerializeField] private WeaponSway _sway;
        [SerializeField] private WeaponView _view;

        protected Attack _attack;
        protected bool _isReadyToShoot = true;
        
        public WeaponRecoil Recoil => _recoil;
        public WeaponBobbing Bobbing => _bobbing;
        public WeaponSway Sway => _sway;
        public WeaponView View => _view;
        public bool IsReadyToShoot => _isReadyToShoot;
        public bool CanHold => _canHold;

        public virtual void Initialize(Transform cameraTransform, IRecoilControlAngles recoilControlAngles)
        {
            transform.localPosition = _positionInHolder;
            _recoil.Initialize(_positionInHolder, recoilControlAngles);
        }

        public virtual void Initialize() {}

        public abstract IEnumerator PerformAttack();
        public abstract IEnumerator PerformAttack(Transform target);

        protected void Shoot()
        {
            _attack.Perform();
            _view.PlayTrail(_muzzle.position, _attack.HitPosition);

            if (_attack.IsHit)
            {
                _view.PlayImpactParticles(_attack.HitPosition, _attack.HitNormal, _attack.HitLayer);
            }
        }
        
        protected void Shoot(Transform target)
        {
            _attack.Perform(target);
            _view.PlayTrail(_muzzle.position, _attack.HitPosition);
        }
    }
}