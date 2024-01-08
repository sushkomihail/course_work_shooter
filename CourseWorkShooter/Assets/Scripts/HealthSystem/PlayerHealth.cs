using System.Collections;
using UnityEngine;

namespace HealthSystem
{
    public class PlayerHealth : Health
    {
        [SerializeField] private int _maxArmor = 50;
        [SerializeField] private float _recoveryCooldownTime = 3;
        [SerializeField] private float _recoverySpeed = 0.5f;

        private float _currentArmor;
        private IEnumerator _recoveryRoutine;

        public override void Initialize()
        {
            base.Initialize();
            _currentArmor = _maxArmor;
            _recoveryRoutine = RecoverHealth();
        }
        
        public override void TakeDamage(int damage)
        {
            StopCoroutine(_recoveryRoutine);
            
            float armorDamage = damage >= _currentArmor ? _currentArmor : damage;
            float healthDamage = damage - armorDamage;

            _currentArmor -= armorDamage;
            _currentHealth -= healthDamage;

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
                yield return null;
            }

            _currentHealth = _maxHealth;
        }
    }
}