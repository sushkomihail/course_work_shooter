using System;
using HealthSystem;
using UnityEngine;

namespace AttackSystem
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private int _damage;

        public void Initialize(Vector3 moveDirection, float force, int damage)
        {
            _damage = damage;
            Instantiate(gameObject);
            _rigidbody.AddForce(moveDirection * force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.root.TryGetComponent(out Health health)) return;
                
            if (health.IsDied) return;

            health.TakeDamage(_damage);
        }
    }
}