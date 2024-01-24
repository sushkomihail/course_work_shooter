using PauseSystem;
using SaveSystem;
using SaveSystem.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Music
{
    public class MusicManager : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] _clips;

        private PauseManager _pauseManager => GameManager.Instance.PauseManager;

        private void Awake()
        {
            SetVolume();
            
            _pauseManager.AddHandler(this);
        }

        private void Update()
        {
            if (_source == null) return;
            
            if (_source.isPlaying) return;

            _source.clip = GetRandomClip();
            _source.Play();
        }

        public void OnPause(bool isPaused)
        {
            if (_source == null) return;

            if (isPaused)
            {
                _source.Pause();    
            }
            else
            {
                _source.UnPause();
            }
        }

        private AudioClip GetRandomClip()
        {
            int index = Random.Range(0, _clips.Length - 1);
            return _clips[index];
        }

        private void SetVolume()
        {
            if (_source == null) return;

            SoundData data = Saver<SoundData>.Load(DataTypes.Sound);
            
            if (data == null) return;

            _source.volume = data.MusicVolume;
        }
    }
}