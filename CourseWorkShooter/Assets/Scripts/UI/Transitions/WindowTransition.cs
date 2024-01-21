using UnityEngine;

namespace UI.Transitions
{
    public class WindowTransition : Transition
    {
        [SerializeField] private GameObject _currentWindow;
        [SerializeField] private GameObject _targetWindow;

        protected override void Perform()
        {
            _currentWindow.SetActive(false);
            _targetWindow.SetActive(true);
        }
    }
}