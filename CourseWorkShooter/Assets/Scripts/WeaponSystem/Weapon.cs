using System.Collections;
using AttackSystem;
using Player;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] protected Transform _muzzle;
        [SerializeField] protected Vector2 _spreadRange = new Vector2(0.02f, 0.02f);
        [SerializeField] protected LayerMask _attackMask;
        [SerializeField] protected int _damage = 20;
        [SerializeField] protected float _shotCooldownTime = 0.2f;
        [SerializeField] private Vector3 _positionInHolder;
        [SerializeField] private bool _canHold;
        
        [Header("Systems")]
        [SerializeField] protected WeaponEffects _effects;
        [SerializeField] private WeaponRecoil _recoil;
        [SerializeField] private WeaponBobbing _bobbing;
        [SerializeField] private WeaponSway _sway;

        protected Attack _attack;
        
        public WeaponRecoil Recoil => _recoil;
        public WeaponBobbing Bobbing => _bobbing;
        public WeaponSway Sway => _sway;
        public bool CanHold => _canHold;
        public bool IsReadyToShoot { get; protected set; }

        public virtual void Initialize(Transform cameraTransform, IRecoilControlAngles recoilControlAngles)
        {
            IsReadyToShoot = true;
            
            transform.localPosition = _positionInHolder;
            
            _effects.Initialize();
            _recoil.Initialize(_positionInHolder, recoilControlAngles);
        }

        public virtual void Initialize()
        {
            IsReadyToShoot = true;
        }

        public abstract IEnumerator Shoot();
        public abstract IEnumerator Shoot(Transform target);
    }
}