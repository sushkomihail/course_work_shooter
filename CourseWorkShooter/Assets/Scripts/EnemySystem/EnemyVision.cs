using Player;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private float _fov;
        
        public Transform PlayerTransform { get; private set; }

        public void Initialize()
        {
            PlayerTransform = FindObjectOfType<PlayerController>().transform;
        }
        
        public bool TryFindPlayer()
        {
            if (PlayerTransform == null) return false;

            Vector3 directionToPlayer = PlayerTransform.position - transform.position;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer > _fov / 2) return false;
            
            Ray ray = new Ray(transform.position, directionToPlayer);

            if (Physics.Raycast(ray, out RaycastHit hit))
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
            return transform.forward * Mathf.Cos(angle) + transform.right * Mathf.Sin(angle);
        }

        private void OnDrawGizmos()
        {
            Vector3 leftVector = CalculateFovVector(_fov * Mathf.Deg2Rad / 2) * 5;
            Vector3 rightVector = CalculateFovVector(-_fov * Mathf.Deg2Rad / 2) * 5;
            Gizmos.DrawRay(transform.position, leftVector);
            Gizmos.DrawRay(transform.position, rightVector);
        }
    }
}