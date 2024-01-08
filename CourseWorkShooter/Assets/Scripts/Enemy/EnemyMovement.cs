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
        [SerializeField] private float _speed = 2;

        private int _isWalkingHash;
        private int _locomotionHash;
         
        private Transform _defaultTarget;

        public Transform CurrentTarget { get; private set; }

        public void Initialize(float attackDistance)
        {
            _isWalkingHash = Animator.StringToHash("IsWalking");
            _locomotionHash = Animator.StringToHash("Locomotion");
            
            _agent.speed = _speed;
            _agent.stoppingDistance = attackDistance;
            
            _defaultTarget = FindObjectOfType<ChestController>().transform;
        }

        public void MoveToTarget(EnemyVision vision)
        {
            CurrentTarget = _defaultTarget;
            float distanceToChest = Vector3.Distance(transform.position, _defaultTarget.position);
            
            if (vision.TryFindPlayer())
            {
                float distanceToPlayer = Vector3.Distance(transform.position, vision.PlayerTransform.position);

                if (distanceToPlayer < distanceToChest)
                {
                    CurrentTarget = vision.PlayerTransform;
                }
            }
            
            _agent.SetDestination(CurrentTarget.position);
            _animator.SetBool(_isWalkingHash, _agent.velocity.magnitude > _transitionalValue);
        }
    }
}