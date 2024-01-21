using UnityEngine;

namespace ImpactSystem
{
    [CreateAssetMenu(menuName = "Collections/Impacts", fileName = "Impacts")]
    public class ImpactsCollection : ScriptableObject
    {
        [SerializeField] private ImpactsCollectionItem[] _impacts;

        public Impact GetImpact(LayerMask hitLayer)
        {
            foreach (ImpactsCollectionItem impact in _impacts)
            {
                if ((impact.WorkLayer.value & (1 << hitLayer)) == 0) continue;

                if (impact.ImpactPrefab != null)
                {
                    return impact.ImpactPrefab;
                }
            }

            return null;
        }
    }
}