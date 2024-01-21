using UnityEngine;
using UnityEngine.UI;
using WeaponSystem;

namespace UI
{
    public class WeaponsSelectionItem : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Transform _weaponParent;
        [SerializeField] private Button _selectionButton;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private float _rotationSpeed = 15;
        [SerializeField] private Weapon _weaponPrefab;

        private bool _isSelected;
        
        public static int SelectedCount { get; private set; }

        private void OnEnable()
        {
            WeaponsProvider.Initialize();
            SelectedCount = 0;
            _background.color = _defaultColor;
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        }

        private void Update()
        {
            RotateWeapon();
        }

        private void RotateWeapon()
        {
            _weaponParent.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        }

        private void OnSelectionButtonClick()
        {
            _isSelected = !_isSelected;

            if (_isSelected)
            {
                WeaponsProvider.Add(_weaponPrefab);
                SelectedCount += 1;
                _background.color = _selectedColor;
            }
            else
            {
                WeaponsProvider.Remove(_weaponPrefab);
                SelectedCount -= 1;
                _background.color = _defaultColor;
            }
        }
    }
}