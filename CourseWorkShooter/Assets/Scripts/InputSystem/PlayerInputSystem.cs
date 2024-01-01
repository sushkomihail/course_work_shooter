namespace InputSystem
{
    public class PlayerInputSystem : InputSystem
    {
        public override void OnActivate()
        {
            Controls.Player.Enable();
        }

        public override void OnDisactivate()
        {
            Controls.Player.Disable();
        }
    }
}