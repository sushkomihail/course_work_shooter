using System.Collections;
using UI;
using UnityEngine;

namespace HealthSystem
{
    public class PlayerHealth : Health
    {
        [SerializeField] private int _maxArmor = 50;
        [SerializeField] private float _recoveryCooldownTime = 3;
        [SerializeField] private float _recoverySpeed = 0.5f;

        private int _currentArmor;
        private IEnumerator _recoveryRoutine;
        
        private float _armorFraction => (float)_currentArmor / _maxArmor;
        private float _healthFraction => _currentHealth / _maxHealth;

        public override void Initialize()
        {
            base.Initialize();
            _currentArmor = _maxArmor;
            _recoveryRoutine = RecoverHealth();
        }
        
        public override void TakeDamage(int damage)
        {
            StopCoroutine(_recoveryRoutine);
            
            int armorDamage = damage >= _currentArmor ? _currentArmor : damage;
            int healthDamage = damage - armorDamage;

            _currentArmor -= armorDamage;
            _currentHealth -= healthDamage;
            
            PlayerUi.Instance.UpdatePlayerConditions(_armorFraction, _healthFraction);

            if (_currentHealth <= 0)
            {
                GameManager.Instance.EndGame();
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
            PlayerUi.Instance.UpdatePlayerConditions(_armorFraction, _healthFraction);
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
                PlayerUi.Instance.UpdatePlayerConditions(_armorFraction, _healthFraction);
                yield return null;
            }
        
            _currentHealth = _maxHealth;
            PlayerUi.Instance.UpdatePlayerConditions(_armorFraction, _healthFraction);
        }
    }
}