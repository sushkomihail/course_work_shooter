using Collections;
using ImpactSystem;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private int _emittingParticlesCount = 5;
        [SerializeField] private Trail _trailPrefab;
        [SerializeField] private ImpactsCollection _impacts;

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

        public void PlayImpactParticles(Vector3 hitPosition, Vector3 hitNormal, LayerMask hitLayer)
        {
            Impact impactPrefab = _impacts.GetImpact(hitLayer);
            
            if (impactPrefab == null) return;

            Quaternion rotation = Quaternion.LookRotation(hitNormal);
            Instantiate(impactPrefab, hitPosition, rotation);
        }
    }
}
