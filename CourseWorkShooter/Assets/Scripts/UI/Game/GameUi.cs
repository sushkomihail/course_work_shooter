using HealthSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class GameUi : MonoBehaviour
    {
        [Header("Player UI")]
        [SerializeField] private Image _playerHealthBar;
        [SerializeField] private Image _playerArmorBar;
        
        [Header("Chest UI")]
        [SerializeField] private Image _chestHealthBar;
        
        [Header("Score UI")]
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Animation _scoreAnimation;
        
        [Header("Timer UI")]
        [SerializeField] private TextMeshProUGUI _timer;

        private PlayerUi _playerUi;
        private ScoreUi _scoreUi;
        private ChestUi _chestUi;

        private void Awake()
        {
            _playerUi = new PlayerUi(_playerHealthBar, _playerArmorBar);
            _scoreUi = new ScoreUi(_score, _scoreAnimation);
            _chestUi = new ChestUi(_chestHealthBar);

            PlayerHealth.OnHealthChanged.AddListener(_playerUi.Update);
            EventManager.OnEnemyDeath.AddListener(_scoreUi.Update);
            ChestHealth.OnHealthChanged.AddListener(_chestUi.Update);
        }
    }
}