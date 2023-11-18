using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _muzzle;
        [SerializeField] protected Vector2 _spreadRange;
        [SerializeField] protected LayerMask _attackMask;
        [SerializeField] protected int _damage;
        [SerializeField] private Vector3 _positionInHolder; // !!!!!!!!!
        [SerializeField] private bool _useTrail; // !!!!!!!!!

        protected Camera _camera;
        protected Attack _attack;
        protected bool _canAttack = true;

        public Transform Muzzle => _muzzle;
        public Attack Attack => _attack;
        public bool CanAttack => _canAttack;
        public bool UseTrail => _useTrail;

        public virtual void Initialize(Camera camera)
        {
            _camera = camera;
            transform.localPosition = _positionInHolder;
        }

        public abstract IEnumerator PerformAttack();
    }
}