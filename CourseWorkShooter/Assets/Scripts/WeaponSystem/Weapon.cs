using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponView _view;
        [SerializeField] protected Transform _muzzle;
        [SerializeField] protected Vector2 _spreadRange;
        [SerializeField] protected LayerMask _attackMask;
        [SerializeField] protected int _damage;
        [SerializeField] protected float _shotCooldownTime;
        [SerializeField] private Vector3 _positionInHolder;
        [SerializeField] private bool _canHold;

        protected Attack _attack;
        protected bool _isReadyToShoot = true;

        public WeaponView View => _view;

        public bool IsReadyToShoot => _isReadyToShoot;
        
        public bool CanHold => _canHold;

        public virtual void Initialize(Camera camera)
        {
            transform.localPosition = _positionInHolder;
        }

        public abstract IEnumerator PerformAttack();
    }
}