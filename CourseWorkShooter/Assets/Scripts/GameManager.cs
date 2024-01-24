using InputSystem;
using PauseSystem;
using SaveSystem;
using UI.Game;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiInput _input;
    [SerializeField] private GameUi _gameUi;

    public static GameManager Instance;

    public bool IsGameEnd;

    public PauseManager PauseManager { get; private set; }

    private void Awake()
    {
        Instance = this;
        
        PauseManager = new PauseManager();
        
        _input.Initialize();
        _gameUi.Initialize();
        
        _input.Controls.UI.Pause.performed += _ =>
        {
            if (!IsGameEnd) PauseManager.OnPause();
        };
        
        EventManager.OnGameEnd.AddListener(OnGameEnd);
    }

    private void Update()
    {
        if (PauseManager.IsPaused || IsGameEnd) return;
        
        _gameUi.TimerUi.Update();
    }

    private void OnEnable() => _input.Enable();

    private void OnDisable() => _input.Disable();

    private void OnGameEnd()
    {
        IsGameEnd = true;
        SaveStats();
        PauseManager.OnPause();
        _gameUi.ShowResults();
    }
    
    private void SaveStats()
    {
        StatsData data = Saver<StatsData>.Load(DataTypes.Stats);
        int score = _gameUi.ScoreUi.Score;
        float time = _gameUi.TimerUi.Time;

        if (data != null)
        {
            score = Mathf.Max(score, data.Score);
            time = Mathf.Max(time, data.Time);
        }
        
        data = new StatsData(score, time);
        Saver<StatsData>.Save(data, DataTypes.Stats);
    }
}