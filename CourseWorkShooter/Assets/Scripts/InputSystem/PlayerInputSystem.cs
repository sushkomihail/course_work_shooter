namespace InputSystem
{
    public class PlayerInputSystem : InputSystem
    {
        protected override void OnEnable()
        {
            Controls.Player.Enable();
        }

        protected override void OnDisable()
        {
            Controls.Player.Disable();
        }
    }
}