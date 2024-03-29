﻿using BuffSystem;
using UnityEngine;
using UnityEngine.UI;

namespace HealthSystem
{
    public class EnemyHealth : Health
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Buff _Buff;
        
        public override void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            float healthFraction = _currentHealth / _maxHealth;
            _healthBar.fillAmount = healthFraction;

            if (_currentHealth <= 0)
            {
                IsDied = true;
                Instantiate(_Buff, transform.position, transform.rotation);
                Destroy(gameObject);
                EventManager.OnEnemyDeath.Invoke();
            }
        }
    }
}