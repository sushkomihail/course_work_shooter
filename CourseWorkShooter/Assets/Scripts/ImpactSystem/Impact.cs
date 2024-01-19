using UnityEngine;

namespace ImpactSystem
{
    public class Impact : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }
    }
}
