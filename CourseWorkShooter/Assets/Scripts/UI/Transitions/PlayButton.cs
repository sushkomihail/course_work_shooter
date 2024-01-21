namespace UI.Transitions
{
    public class PlayButton : SceneTransition
    {
        protected override void Perform()
        {
            if (WeaponsSelectionItem.SelectedCount == 0) return;
            
            base.Perform();
        }
    }
}