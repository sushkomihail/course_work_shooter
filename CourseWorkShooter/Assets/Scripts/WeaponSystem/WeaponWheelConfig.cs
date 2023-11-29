using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "WeaponSystem/WeaponsWheel", fileName = "WeaponsWheelConfig")]
    public class WeaponWheelConfig : ScriptableObject
    {
        [SerializeField] private WeaponWheelItem[] _items;

        public List<Weapon> FindWeapons(int[] weaponIdentificators)
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (int id in weaponIdentificators)
            {
                weapons.Add(_items[id].Weapon);
            }

            return weapons;
        }
    }
}