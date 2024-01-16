using UnityEngine;

namespace BuffSystem
{
    public abstract class Buff : MonoBehaviour
    {
        [SerializeField] protected int _buffValue;

        protected abstract void Perform(Collider other);

        private void OnTriggerEnter(Collider other)
        {
            Perform(other);
        }
    }
}