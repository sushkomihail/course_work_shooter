using System;
using UnityEngine;

namespace WeaponSystem
{
    [Serializable]
    public class WeaponWheelItem
    {
        [SerializeField] private Weapon _weapon;

        private static int _currentId;
        private int _id;
        
        public int Id => _id;
        public Weapon Weapon => _weapon;

        public WeaponWheelItem()
        {
            AssignId();
        }

        private void AssignId()
        {
            _id = _currentId++;
        }
    }
}