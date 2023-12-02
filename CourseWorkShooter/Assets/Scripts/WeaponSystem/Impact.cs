using UnityEngine;

namespace WeaponSystem
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
