using PauseSystem;
using UnityEngine;

namespace UI.Game
{
    public class PauseUi : IPauseHandler
    {
        private readonly GameObject _pauseWindow;
        private readonly GameObject _gameWindow;

        public PauseUi(GameObject pauseWindow, GameObject gameWindow)
        {
            _pauseWindow = pauseWindow;
            _gameWindow = gameWindow;
        }

        public void OnPause(bool isPaused)
        {
            if (_pauseWindow == null) return;
            
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isPaused;
            _gameWindow.SetActive(!isPaused);
            _pauseWindow.SetActive(isPaused && !GameManager.Instance.IsGameEnd);
        }
    }
}