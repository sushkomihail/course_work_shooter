using System;
using UnityEngine;

namespace ImpactSystem
{
    [Serializable]
    public class ImpactsCollectionItem
    {
        [SerializeField] private LayerMask _workLayer;
        [SerializeField] private Impact _impactPrefab;

        public LayerMask WorkLayer => _workLayer;
        public Impact ImpactPrefab => _impactPrefab;
    }
}