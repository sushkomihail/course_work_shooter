using UnityEngine;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Vector3 _positionInHolder;
        [SerializeField] private Transform _startAttackPoint;
        [SerializeField] private float _attackDistance;
        [SerializeField] private Vector2 _spreadRange;
        [SerializeField] private LayerMask _attackMask;
        [SerializeField] private int _damage;

        public Vector3 PositionInHolder => _positionInHolder;

        protected Camera _camera;
        protected Attack _attack;
        
        
    }
}