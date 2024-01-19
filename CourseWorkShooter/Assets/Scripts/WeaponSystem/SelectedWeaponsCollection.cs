using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "Collections/SelectedWeaponsCollection", fileName = "SelectedWeaponsCollection")]
    public class SelectedWeaponsCollection : ScriptableObject
    {
        private List<Weapon> _weaponPrefabs;

        public Weapon[] WeaponPrefabs => _weaponPrefabs.ToArray();

        public void Initialize()
        {
            if (_weaponPrefabs == null)
            {
                _weaponPrefabs = new List<Weapon>();
            }
            else
            {
                _weaponPrefabs.Clear();
            }
        }

        public void Add(Weapon weaponPrefab)
        {
            if (!_weaponPrefabs.Contains(weaponPrefab))
            {
                _weaponPrefabs.Add(weaponPrefab);
            }
        }

        public void Remove(Weapon weaponPrefab)
        {
            if (_weaponPrefabs.Contains(weaponPrefab))
            {
                _weaponPrefabs.Remove(weaponPrefab);
            }
        }
    }
}