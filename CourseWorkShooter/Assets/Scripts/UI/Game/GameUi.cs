using HealthSystem;
using PauseSystem;
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

        [Header("Pause UI")]
        [SerializeField] private GameObject _pauseWindow;
        [SerializeField] private GameObject _gameWindow;
        
        [Header("Timer UI")]
        [SerializeField] private TextMeshProUGUI _timer;

        [Header("Results")]
        [SerializeField] private GameObject _resultsWindow;
        [SerializeField] private TextMeshProUGUI _scoreResult;
        [SerializeField] private TextMeshProUGUI _timeResult;
        
        private PlayerUi _playerUi;
        private ChestUi _chestUi;
        private PauseUi _pauseUi;
        private ResultsUi _resultsUi;

        public ScoreUi ScoreUi { get; private set; }
        public TimerUi TimerUi { get; private set; }

        private PauseManager _pauseManager => GameManager.Instance.PauseManager;

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _playerUi = new PlayerUi(_playerHealthBar, _playerArmorBar);
            _chestUi = new ChestUi(_chestHealthBar);
            _pauseUi = new PauseUi(_pauseWindow, _gameWindow);
            _resultsUi = new ResultsUi(_resultsWindow, _scoreResult, _timeResult);
            ScoreUi = new ScoreUi(_score, _scoreAnimation);
            TimerUi = new TimerUi(_timer);
            

            PlayerHealth.OnHealthChanged.AddListener(_playerUi.Update);
            ChestHealth.OnHealthChanged.AddListener(_chestUi.Update);
            EventManager.OnEnemyDeath.AddListener(ScoreUi.Update);

            _pauseManager.AddHandler(_pauseUi);
        }

        public void ShowResults()
        {
            string score = ScoreUi.Score.ToString();
            string time = TimerUi.TimeConvertedToReadableFormat(TimerUi.Time);
            _resultsUi.Update(score, time);
        }
    }
}