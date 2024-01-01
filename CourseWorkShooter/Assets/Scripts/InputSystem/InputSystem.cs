using UnityEngine;

namespace InputSystem
{
    public abstract class InputSystem : MonoBehaviour
    {
        public Controls Controls { get; private set; }
        
        public abstract void OnActivate();

        public abstract void OnDisactivate();

        public void Initialize()
        {
            Controls = new Controls();
        }
    }
}
