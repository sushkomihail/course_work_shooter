using HealthSystem;
using UnityEngine;

namespace BuffSystem
{
    public class ArmorBuff : Buff
    {
        protected override void Perform(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.RepairArmor(_buffValue);
                Destroy(gameObject);
            }
        }
    }
}
