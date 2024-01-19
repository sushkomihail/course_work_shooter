using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WeaponSystem;

namespace UI
{
    public class WeaponGridItem : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _background;
        [SerializeField] private Transform _weaponParent;
        [SerializeField] private Button _selectButton;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private float _defaultWeaponParentScale = 400;
        [SerializeField] private float _rotationSpeed = 15;
        
        private UiGrid _grid;
        private Weapon _weaponPrefab;
        private bool _isSelected;
        
        public RectTransform RectTransform => _rectTransform;

        private void Update()
        {
            RotateWeapon();
        }

        public void Initialize(UiGrid grid, WeaponsCollectionItem weaponItem, float scaleFraction)
        {
            _background.color = _defaultColor;
            
            _grid = grid;

            _weaponPrefab = weaponItem.WeaponPrefab;
            Weapon weapon = Instantiate(_weaponPrefab, _weaponParent);
            weapon.transform.localRotation = Quaternion.Euler(0, 90, 0);

            _name.text = weaponItem.Name;
            
            _weaponParent.localScale = Vector3.one * _defaultWeaponParentScale;
            _weaponParent.localScale *= scaleFraction;
            
            _selectButton.onClick.AddListener(() =>
            {
                _isSelected = !_isSelected;

                if (_isSelected)
                {
                    _background.color = _selectedColor;
                    _grid.SelectWeapon(_weaponPrefab);
                }
                else
                {
                    _background.color = _defaultColor;
                    _grid.DeselectWeapon(_weaponPrefab);
                }
            });
        }

        private void RotateWeapon()
        {
            _weaponParent.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        }
    }
}