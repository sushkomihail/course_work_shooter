namespace InputSystem
{
    public class UiInput : InputSystem
    {
        public override void Enable()
        {
            Controls.UI.Enable();
        }

        public override void Disable()
        {
            Controls.UI.Disable();
        }
    }
}