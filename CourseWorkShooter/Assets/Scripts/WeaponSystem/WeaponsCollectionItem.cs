using System;
using UnityEngine;

namespace WeaponSystem
{
    [Serializable]
    public class WeaponsCollectionItem
    {
        [SerializeField] private string _name;
        [SerializeField] private Weapon _weaponPrefab;

        public string Name => _name;
        public Weapon WeaponPrefab => _weaponPrefab;
    }
}