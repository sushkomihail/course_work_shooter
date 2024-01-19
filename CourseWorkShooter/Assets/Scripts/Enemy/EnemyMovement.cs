using Chest;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _transitionalValue = 0.5f;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _moveSpeed = 2;
        [SerializeField] private float _rotationSpeed = 2;

        private int _isWalkingHash;

        public void Initialize(float stoppingDistance)
        {
            _isWalkingHash = Animator.StringToHash("IsWalking");
            
            _agent.speed = _moveSpeed;
            _agent.stoppingDistance = stoppingDistance;
        }

        public void Move(Transform target)
        {
            RotateToTarget(target);
            _agent.SetDestination(target.position);
            _animator.SetBool(_isWalkingHash, _agent.velocity.magnitude > _transitionalValue);
        }

        private void RotateToTarget(Transform target)
        {
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}