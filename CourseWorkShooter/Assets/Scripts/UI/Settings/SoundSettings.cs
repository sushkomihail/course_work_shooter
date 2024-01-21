using SaveSystem.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;

        public SoundData Data => new SoundData(
            _musicVolumeSlider.value,
            _effectsVolumeSlider.value
        );

        public void Initialize(SoundData soundData)
        {
            if (soundData == null) return;
            
            _musicVolumeSlider.value = soundData.MusicVolume;
            _effectsVolumeSlider.value = soundData.EffectsVolume;
        }
    }
}