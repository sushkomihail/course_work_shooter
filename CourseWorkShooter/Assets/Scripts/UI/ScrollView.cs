using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ScrollView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private ScrollViewItemsCollection itemsCollectionCollection;
        [SerializeField] private RectTransform _itemsParent;
        [SerializeField] private float _spacing = 50;
        [SerializeField] private float _scrollingOutOfBounds = 700;
        [SerializeField] private float _snapSpeed = 5;

        private int _itemsCount;
        private RectTransform[] _items;
        private float[] _itemXPositions;
        private float _minXPosition;
        private float _maxXPosition;
        private int _selectedItemIndex;
        private bool _isScrolling;
        private float _snapFraction;

        private void Awake()
        {
            InstantiateItems();
        }

        private void Update()
        {
            CalculateSelectedItemIndex();
            ClampScrolling();
            TrySnapItem();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isScrolling = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isScrolling = false;
            _snapFraction = 0;
        }

        private void InstantiateItems()
        {
            if (itemsCollectionCollection.Items.IsEmpty()) return;
            
            _itemsCount = itemsCollectionCollection.Items.Length;
            _items = new RectTransform[_itemsCount];
            _itemXPositions = new float[_itemsCount];
            
            for (int i = 0; i < _itemsCount; i++)
            {
                _items[i] = Instantiate(itemsCollectionCollection.Items[i], _itemsParent);

                float xPosition = _spacing * i + _items[i].sizeDelta.x * i;
                Vector2 itemPosition = new Vector2(xPosition, 0);
                _items[i].anchoredPosition = itemPosition;
                _itemXPositions[i] = -xPosition;
            }

            _minXPosition = _itemXPositions[_itemsCount - 1] - _scrollingOutOfBounds;
            _maxXPosition = _itemXPositions[0] + _scrollingOutOfBounds;
        }
        
        private void ClampScrolling()
        {
            if (_itemsParent.anchoredPosition.x <= _minXPosition)
            {
                _itemsParent.anchoredPosition = new Vector2(_minXPosition, 0);
            }
            else if (_itemsParent.anchoredPosition.x >= _maxXPosition)
            {
                _itemsParent.anchoredPosition = new Vector2(_maxXPosition, 0);
            }
        }

        private void CalculateSelectedItemIndex()
        {
            float minXPositionDelta = float.MaxValue;

            for (int i = 0; i < _itemsCount; i++)
            {
                float delta = Mathf.Abs(_itemsParent.anchoredPosition.x - _itemXPositions[i]);

                if (delta < minXPositionDelta)
                {
                    minXPositionDelta = delta;
                    _selectedItemIndex = i;
                }
            }
        }

        private void TrySnapItem()
        {
            if (_isScrolling) return;

            _snapFraction += _snapSpeed * Time.deltaTime;
            Vector2 targetPosition = new Vector2(_itemXPositions[_selectedItemIndex], 0);
            _itemsParent.anchoredPosition =
                Vector2.Lerp(_itemsParent.anchoredPosition, targetPosition, _snapFraction);
        }
    }
}