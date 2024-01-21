using AttackSystem;
using ImpactSystem;
using SaveSystem;
using SaveSystem.Settings;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponEffects : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioSource _shootAudioSource;
        
        [Header("Particles")]
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private int _emittingParticlesCount = 5;
        
        [Header("Bullet trail")]
        [SerializeField] private Trail _bulletTrailPrefab;
        
        [Header("Impacts")]
        [SerializeField] private ImpactsCollection _impacts;

        public void Initialize()
        {
            SoundData soundData = Saver<SoundData>.Load(DataTypes.Sound);

            if (soundData != null)
            {
                _shootAudioSource.volume *= soundData.EffectsVolume;
            }
        }

        public void ShowShotEffects()
        {
            PlayMuzzleFlashParticles();
            PlayShotSound();
        }
        
        public void ShowBulletEffects(Transform muzzle, Attack attack)
        {
            ShowBulletTrail(muzzle.position, attack.HitPosition);
            
            if (attack.IsHit)
            {
                PlayImpactParticles(attack.HitPosition, attack.HitNormal, attack.HitLayer);
            }
        }

        private void PlayShotSound()
        {
            if (_shootAudioSource == null) return;
            
            _shootAudioSource.Play();
        }
        
        private void PlayMuzzleFlashParticles()
        {
            if (_muzzleFlash == null) return;

            if (!_muzzleFlash.isPlaying)
            {
                _muzzleFlash.Emit(_emittingParticlesCount);
            }
        }

        private void ShowBulletTrail(Vector3 startPoint, Vector3 endPoint)
        {
            if (_bulletTrailPrefab == null) return;
            
            Trail trail = Instantiate(_bulletTrailPrefab);
            trail.StartCoroutine(trail.ShowTrail(startPoint, endPoint));
        }

        private void PlayImpactParticles(Vector3 hitPosition, Vector3 hitNormal, LayerMask hitLayer)
        {
            Impact impactPrefab = _impacts.GetImpact(hitLayer);
            
            if (impactPrefab == null) return;

            Quaternion rotation = Quaternion.LookRotation(hitNormal);
            Instantiate(impactPrefab, hitPosition, rotation);
        }
    }
}
