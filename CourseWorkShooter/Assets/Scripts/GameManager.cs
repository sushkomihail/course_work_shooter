using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _targetFps;
    [SerializeField] private GameObject _gameUi;
    [SerializeField] private GameObject _resultsWindow;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        Application.targetFrameRate = _targetFps;
    }

    public void EndGame()
    {
        StopAllCoroutines();
        ShowGameResults();
    }

    private void ShowGameResults()
    {
        _gameUi.SetActive(false);
        _resultsWindow.SetActive(true);
    }
}