using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "WeaponSystem/WeaponsCollection", fileName = "WeaponsCollection")]
    public class WeaponsCollection : ScriptableObject
    {
        [SerializeField] private Weapon[] _weapons;

        public Weapon[] Weapons => _weapons;
    }
}