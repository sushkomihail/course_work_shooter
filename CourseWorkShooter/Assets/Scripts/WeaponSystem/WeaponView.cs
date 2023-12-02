using UnityEngine;

namespace WeaponSystem
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private int _emittingParticlesCount = 5;
        [SerializeField] private Trail _trailPrefab;
        [SerializeField] private Impact _impactPrefab;

        public void PlayMuzzleFlashParticles()
        {
            if (_muzzleFlash == null) return;

            if (!_muzzleFlash.isPlaying)
            {
                _muzzleFlash.Emit(_emittingParticlesCount);
            }
        }
        
        public void PlayShootSound()
        {
            if (_shootAudioSource == null) return;
            
            _shootAudioSource.Play();
        }

        public void PlayTrail(Vector3 startPoint, Vector3 endPoint)
        {
            if (_trailPrefab == null) return;
            
            Trail trail = Instantiate(_trailPrefab);
            trail.StartCoroutine(trail.ShowTrail(startPoint, endPoint));
        }

        public void PlayImpactParticles(Vector3 hitPosition, Vector3 hitNormal)
        {
            if (_impactPrefab == null) return;

            Quaternion rotation = Quaternion.LookRotation(hitNormal);
            Instantiate(_impactPrefab, hitPosition, rotation);
        }
    }
}
