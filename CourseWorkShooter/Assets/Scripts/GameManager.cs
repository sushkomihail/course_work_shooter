using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _targetFps;
    
    private void Awake()
    {
        Application.targetFrameRate = _targetFps;
    }
}