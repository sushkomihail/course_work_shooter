using UnityEngine;

namespace InputSystem
{
    public abstract class InputSystem : MonoBehaviour
    {
        public Controls Controls { get; private set; }
        
        public abstract void Enable();

        public abstract void Disable();

        public void Initialize()
        {
            Controls = new Controls();
        }
    }
}
