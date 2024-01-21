using UnityEngine;
using UnityEngine.UI;

namespace UI.Transitions
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(Perform);
        }

        protected abstract void Perform();
    }
}