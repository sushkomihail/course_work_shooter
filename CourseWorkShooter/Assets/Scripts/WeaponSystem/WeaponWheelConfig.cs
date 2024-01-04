using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "WeaponSystem/WeaponsWheel", fileName = "WeaponsWheelConfig")]
    public class WeaponWheelConfig : ScriptableObject
    {
        [SerializeField] private Weapon[] _weapons;

        public Weapon[] Weapons => _weapons;
    }
}