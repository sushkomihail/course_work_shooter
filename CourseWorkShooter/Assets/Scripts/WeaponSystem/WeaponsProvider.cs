using System.Collections.Generic;

namespace WeaponSystem
{
    public static class WeaponsProvider
    {
        private static List<Weapon> _selectedWeapons;

        public static Weapon[] SelectedWeapons => _selectedWeapons.ToArray();

        public static void Initialize()
        {
            if (_selectedWeapons == null)
            {
                _selectedWeapons = new List<Weapon>();
            }
            else
            {
                _selectedWeapons.Clear();
            }
        }

        public static void Add(Weapon weaponPrefab)
        {
            if (!_selectedWeapons.Contains(weaponPrefab))
            {
                _selectedWeapons.Add(weaponPrefab);
            }
        }

        public static void Remove(Weapon weaponPrefab)
        {
            if (_selectedWeapons.Contains(weaponPrefab))
            {
                _selectedWeapons.Remove(weaponPrefab);
            }
        }
    }
}