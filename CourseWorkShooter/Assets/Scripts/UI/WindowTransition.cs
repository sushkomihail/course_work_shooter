using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WindowTransition : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _currentWindow;
        [SerializeField] private GameObject _targetWindow;

        private void Awake()
        {
            _button.onClick.AddListener(ChangeWindow);
        }

        private void ChangeWindow()
        {
            _currentWindow.SetActive(false);
            _targetWindow.SetActive(true);
        }
    }
}