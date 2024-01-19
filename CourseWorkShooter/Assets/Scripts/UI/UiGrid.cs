using UnityEngine;
using WeaponSystem;

namespace UI
{
    public class UiGrid : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private WeaponGridItem _itemPrefab;
        [SerializeField] private WeaponsCollection _weaponsCollection;
        [SerializeField] private SelectedWeaponsCollection _selectedWeapons;
        [SerializeField] private int _rowsCount = 1;
        [SerializeField] private int _columnsCount = 2;
        [SerializeField] private float _spacing = 10;

        private float _cellWidth;
        private float _cellHeight;
        private WeaponGridItem[] _items;

        private void Awake()
        {
            CalculateCellSize();
            CreateItems();
            SetItemsPosition();
            
            _selectedWeapons.Initialize();
        }

        public void SelectWeapon(Weapon weapon) => _selectedWeapons.Add(weapon);
        
        public void DeselectWeapon(Weapon weapon) => _selectedWeapons.Remove(weapon);

        private void CalculateCellSize()
        {
            _cellWidth = (_rectTransform.rect.width - _spacing * (_columnsCount + 1)) / _columnsCount;
            _cellHeight = (_rectTransform.rect.height - _spacing * (_rowsCount + 1)) / _rowsCount;
        }

        private void CreateItems()
        {
            _items = new WeaponGridItem[_weaponsCollection.Weapons.Length];
            
            for (int i = 0; i < _weaponsCollection.Weapons.Length; i++)
            {
                _items[i] = Instantiate(_itemPrefab, transform);
                _items[i].Initialize(this, _weaponsCollection.Weapons[i], 0.7f);
            }
        }

        private void SetItemsPosition()
        {
            if (_items == null) return;

            int cellIndex = 0;
            
            for (int i = 0; i < _rowsCount; i++)
            {
                for (int j = 0; j < _columnsCount; j++)
                {
                    if (cellIndex > _items.Length - 1)
                    {
                        break;
                    }
                    
                    _items[cellIndex].RectTransform.sizeDelta = new Vector2(_cellWidth, _cellHeight);
                    float xPosition = _spacing * (j + 1) + _cellWidth * j;
                    float yPosition = -_spacing * (i + 1) + _cellHeight * i;
                    _items[cellIndex].RectTransform.anchoredPosition = new Vector2(xPosition, yPosition);
                    cellIndex += 1;
                }
            }
        }
    }
}