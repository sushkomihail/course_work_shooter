using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _targetFps;
    
    public static UnityEvent OnGameEnd = new UnityEvent();
    
    private void Awake()
    {
        Application.targetFrameRate = _targetFps;
        
        OnGameEnd.AddListener(End);
    }

    private void End()
    {
        
    }
}