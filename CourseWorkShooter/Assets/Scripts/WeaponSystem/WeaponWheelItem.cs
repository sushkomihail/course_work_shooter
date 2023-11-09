using System;
using UnityEngine;

namespace WeaponSystem
{
    [Serializable]
    public class WeaponWheelItem
    {
        [SerializeField] private Weapon _weapon;

        public Weapon Weapon => _weapon;
        public int Id => _id;

        private static int _nextId;
        private int _id;

        public WeaponWheelItem()
        {
            AssignId();
        }
        
        private void AssignId()
        {
            _id = _nextId++;
        }
    }
}