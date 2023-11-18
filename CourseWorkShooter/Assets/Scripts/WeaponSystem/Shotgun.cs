using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class Shotgun : Weapon
    {
        public override void Initialize(Camera camera)
        {
        }

        public override IEnumerator PerformAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}