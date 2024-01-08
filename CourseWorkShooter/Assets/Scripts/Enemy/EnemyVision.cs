using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private Transform _eye;
        [SerializeField] private float _fov;
        [SerializeField] private LayerMask _visionMask;
        
        public Transform PlayerTransform { get; private set; }

        public void Initialize()
        {
            PlayerTransform = FindObjectOfType<PlayerController>().transform;
        }
        
        public bool TryFindPlayer()
        {
            if (PlayerTransform == null) return false;

            Vector3 directionToPlayer = PlayerTransform.position - _eye.position;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer > _fov / 2) return false;
            
            Ray ray = new Ray(_eye.position, directionToPlayer);

            if (Physics.Raycast(ray, out RaycastHit hit, _visionMask.value))
            {
                if (hit.transform == PlayerTransform)
                {
                    return true;
                }
            }

            return false;
        }

        private Vector3 CalculateFovVector(float angle)
        {
            return _eye.forward * Mathf.Cos(angle) + _eye.right * Mathf.Sin(angle);
        }

        private void OnDrawGizmos()
        {
            if (_eye == null) return;
            
            Vector3 leftVector = CalculateFovVector(_fov * Mathf.Deg2Rad / 2) * 5;
            Vector3 rightVector = CalculateFovVector(-_fov * Mathf.Deg2Rad / 2) * 5;
            Gizmos.DrawRay(_eye.position, leftVector);
            Gizmos.DrawRay(_eye.position, rightVector);
        }
    }
}