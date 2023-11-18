using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "WeaponSystem/WeaponsWheel", fileName = "WeaponsWheelConfig")]
    public class WeaponWheelConfig : ScriptableObject
    {
        [SerializeField] private WeaponWheelItem[] _items;

        public Weapon[] FindWeapons(int[] weaponIdentificators)
        {
            int identificatorsCount = weaponIdentificators.Length;
            Weapon[] weapons = new Weapon[identificatorsCount];

            int idIndex = 0;

            foreach (WeaponWheelItem item in _items)
            {
                int id = weaponIdentificators[idIndex];
                
                if (item.Id == id)
                {
                    weapons[idIndex] = item.Weapon;
                    idIndex += 1;
                }

                if (idIndex == identificatorsCount) break;
            }

            return weapons;
        }
    }
}