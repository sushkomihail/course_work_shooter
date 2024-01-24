using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem
{
    public class PlayerHealth : Health
    {
        [SerializeField] private int _maxArmor = 50;
        [SerializeField] private float _recoveryCooldownTime = 3;
        [SerializeField] private float _recoverySpeed = 0.5f;

        public static readonly UnityEvent<float, float> OnHealthChanged = new UnityEvent<float, float>();
        
        private int _currentArmor;
        private IEnumerator _recoveryRoutine;
        
        private float _armorFraction => (float)_currentArmor / _maxArmor;
        private float _healthFraction => _currentHealth / _maxHealth;

        public override void Initialize()
        {
            base.Initialize();
            
            _currentArmor = _maxArmor;
        }
        
        public override void TakeDamage(int damage)
        {
            if (_recoveryRoutine != null) StopCoroutine(_recoveryRoutine);
            
            int armorDamage = damage >= _currentArmor ? _currentArmor : damage;
            int healthDamage = damage - armorDamage;

            _currentArmor -= armorDamage;
            _currentHealth -= healthDamage;
            
            OnHealthChanged?.Invoke(_healthFraction, _armorFraction);

            if (_currentHealth <= 0)
            {
                EventManager.OnGameEnd.Invoke();
                return;
            }

            if (_currentHealth < _maxHealth)
            {
                _recoveryRoutine = RecoverHealth();
                StartCoroutine(_recoveryRoutine);
            }
        }

        public void RepairArmor(int buffValue)
        {
            int armorLoss = _maxArmor - _currentArmor;
            int buffAmount = armorLoss > buffValue ? buffValue : armorLoss;
            _currentArmor += buffAmount;
            OnHealthChanged?.Invoke(_healthFraction, _armorFraction);
        }

        private IEnumerator RecoverHealth()
        {
            yield return new WaitForSeconds(_recoveryCooldownTime);
        
            float elapsedTime = 0;
            float duration = (_maxHealth - _currentHealth) / _recoverySpeed;
            float startHealthValue = _currentHealth;
        
            while (elapsedTime < duration)
            {
                float lerpFraction = elapsedTime / duration;
                _currentHealth = Mathf.Lerp(startHealthValue, _maxHealth, lerpFraction);
                elapsedTime += _recoverySpeed * Time.deltaTime;
                OnHealthChanged?.Invoke(_healthFraction, _armorFraction);
                yield return null;
            }
        
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_healthFraction, _armorFraction);
        }
    }
}