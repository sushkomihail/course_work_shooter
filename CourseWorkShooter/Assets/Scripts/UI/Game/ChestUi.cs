using UnityEngine.UI;

namespace UI.Game
{
    public class ChestUi
    {
        private readonly Image _healthBar;

        public ChestUi(Image healthBar)
        {
            _healthBar = healthBar;
        }

        public void Update(float healthFraction)
        {
            if (_healthBar != null) _healthBar.fillAmount = healthFraction;
        }
    }
}