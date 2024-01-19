using Chest;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private Transform _eyeTransform;
        [SerializeField] private float _fov;
        [SerializeField] private LayerMask _visionMask;

        private Transform _chestTransform;
        private Transform _playerTransform;

        public Transform CurrentTarget { get; private set; }

        public void Initialize()
        {
            _chestTransform = FindObjectOfType<ChestController>()?.transform;
            _playerTransform = FindObjectOfType<PlayerController>()?.transform;
        }

        public void Look()
        {
            if (_chestTransform != null)
            {
                CurrentTarget = _chestTransform;
            }
            
            float distanceToChest = Vector3.Distance(_eyeTransform.position, _chestTransform.position);
            
            if (TryFindPlayer())
            {
                float distanceToPlayer = Vector3.Distance(_eyeTransform.position, _playerTransform.position);

                if (distanceToPlayer < distanceToChest)
                {
                    CurrentTarget = _playerTransform;
                }
            }
        }
        
        private bool TryFindPlayer()
        {
            if (_playerTransform == null) return false;

            Vector3 directionToPlayer = _playerTransform.position - _eyeTransform.position;
            float angleToPlayer = Vector3.Angle(_eyeTransform.forward, directionToPlayer);

            if (angleToPlayer > _fov / 2) return false;
            
            Ray ray = new Ray(_eyeTransform.position, directionToPlayer);

            if (Physics.Raycast(ray, out RaycastHit hit, _visionMask.value))
            {
                if (hit.transform == _playerTransform)
                {
                    return true;
                }
            }

            return false;
        }

        private Vector3 CalculateFovVector(float angle)
        {
            return _eyeTransform.forward * Mathf.Cos(angle) + _eyeTransform.right * Mathf.Sin(angle);
        }

        private void OnDrawGizmos()
        {
            if (_eyeTransform == null) return;
            
            Vector3 leftVector = CalculateFovVector(_fov * Mathf.Deg2Rad / 2) * 5;
            Vector3 rightVector = CalculateFovVector(-_fov * Mathf.Deg2Rad / 2) * 5;
            Gizmos.DrawRay(_eyeTransform.position, leftVector);
            Gizmos.DrawRay(_eyeTransform.position, rightVector);
        }
    }
}