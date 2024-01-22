using UnityEngine.UI;

namespace UI.Game
{
    public class PlayerUi
    {
        private readonly Image _healthBar;
        private readonly Image _armorBar;
        
        public PlayerUi(Image healthBar, Image armorBar)
        {
            _healthBar = healthBar;
            _armorBar = armorBar;

            Update(1, 1);
        }

        public void Update(float healthFraction, float armorFraction)
        {
            if (_healthBar != null) _healthBar.fillAmount = healthFraction;
            if (_armorBar != null) _armorBar.fillAmount = armorFraction;
        }
    }
}