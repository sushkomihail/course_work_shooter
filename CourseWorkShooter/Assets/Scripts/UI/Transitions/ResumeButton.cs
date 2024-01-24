using PauseSystem;

namespace UI.Transitions
{
    public class ResumeButton : WindowTransition
    {
        private PauseManager _pauseManager => GameManager.Instance.PauseManager;
        
        protected override void Perform()
        {
            base.Perform();
            
            _pauseManager.OnPause();
        }
    }
}