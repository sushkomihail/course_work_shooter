using Chest;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed;

        private Transform _defaultTarget;
        private Transform _currentTarget;

        public Transform CurrentTarget => _currentTarget;

        public void Initialize(float attackDistance)
        {
            _defaultTarget = FindObjectOfType<ChestController>().transform;

            if (_defaultTarget == null)
            {
                _defaultTarget = transform;
            }

            _agent.speed = _speed;
            _agent.stoppingDistance = attackDistance;
        }

        public void MoveToTarget(EnemyVision vision)
        {
            _currentTarget = _defaultTarget;
            // Vector3 directionToTarget = _currentTarget.position - transform.position;
            // directionToTarget.y = transform.position.y;
            // transform.rotation = Quaternion.LookRotation(directionToTarget);
            float distanceToChest = Vector3.Distance(transform.position, _defaultTarget.position);
            
            if (vision.TryFindPlayer())
            {
                float distanceToPlayer = Vector3.Distance(transform.position, vision.PlayerTransform.position);

                if (distanceToPlayer < distanceToChest)
                {
                    _currentTarget = vision.PlayerTransform;
                }
            }
            
            _agent.SetDestination(_currentTarget.position);
        } 
    }
}