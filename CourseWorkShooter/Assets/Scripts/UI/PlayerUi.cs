using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUi : MonoBehaviour
    {
        [SerializeField] private Image _playerArmorBar;
        [SerializeField] private Image _playerHealthBar;
        [SerializeField] private TextMeshProUGUI _score;

        public static PlayerUi Instance;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            
            EventManager.OnEnemyDeath.AddListener(UpdateScore);
        }

        public void UpdatePlayerConditions(float armorFraction, float healthFraction)
        {
            _playerArmorBar.fillAmount = armorFraction;
            _playerHealthBar.fillAmount = healthFraction;
        }

        private void UpdateScore() => _score.text = GameManager.Instance.Score.ToString();
    }
}