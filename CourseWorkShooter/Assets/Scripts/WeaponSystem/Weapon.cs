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

        public bool CanAttack => _canAttack;

        protected Camera _camera;
        protected bool _canAttack = true;

        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public abstract IEnumerator PerformAttack();
    }
}