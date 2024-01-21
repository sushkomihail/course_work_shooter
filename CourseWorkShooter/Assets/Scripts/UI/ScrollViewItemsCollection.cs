using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Collections/ScrollViewItems", fileName = "Items")]
    public class ScrollViewItemsCollection : ScriptableObject
    {
        [SerializeField] private RectTransform[] _items;

        public RectTransform[] Items => _items;
    }
}