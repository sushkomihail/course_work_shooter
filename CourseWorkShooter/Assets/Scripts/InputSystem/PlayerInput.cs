namespace InputSystem
{
    public class PlayerInput : InputSystem
    {
        public override void Enable()
        {
            Controls.Player.Enable();
        }

        public override void Disable()
        {
            Controls.Player.Disable();
        }
    }
}