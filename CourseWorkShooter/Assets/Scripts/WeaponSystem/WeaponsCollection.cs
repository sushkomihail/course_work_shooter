using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "Collections/WeaponsCollection", fileName = "WeaponsCollection")]
    public class WeaponsCollection : ScriptableObject
    {
        [SerializeField] private WeaponsCollectionItem[] _weapons;

        public WeaponsCollectionItem[] Weapons => _weapons;
    }
}