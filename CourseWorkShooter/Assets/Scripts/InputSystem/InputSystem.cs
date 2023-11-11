using UnityEngine;

namespace InputSystem
{
    public abstract class InputSystem : MonoBehaviour
    {
        public PlayerControls Controls { get; private set; }
        
        protected abstract void OnEnable();

        protected abstract void OnDisable();

        public void Initialize()
        {
            Controls = new PlayerControls();
        }
    }
}
