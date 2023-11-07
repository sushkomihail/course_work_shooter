namespace InputSystem
{
    public class PlayerInputSystem : InputSystem
    {
        protected override void OnEnable()
        {
            Controls.Movement.Enable();
        }

        protected override void OnDisable()
        {
            Controls.Movement.Disable();
        }
    }
}