using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _targetFps;
    [SerializeField] private GameObject _gameUi;
    [SerializeField] private GameObject _resultsWindow;

    public static GameManager Instance;
    
    public int Score { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        EventManager.OnEnemyDeath.AddListener(IncreaseScore);
        
        Application.targetFrameRate = _targetFps;
    }

    public void EndGame()
    {
        StopAllCoroutines();
        ShowGameResults();
    }

    private void IncreaseScore() => Score +=  1;

    private void ShowGameResults()
    {
        _gameUi.SetActive(false);
        _resultsWindow.SetActive(true);
    }
}