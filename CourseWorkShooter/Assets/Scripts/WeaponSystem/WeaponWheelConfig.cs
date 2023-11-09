using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "WeaponSystem/WeaponsWheel", fileName = "WeaponsWheelConfig")]
    public class WeaponWheelConfig : ScriptableObject
    {
        [SerializeField] private WeaponWheelItem[] _items;

        public WeaponWheelItem[] Items => _items;
    }
}