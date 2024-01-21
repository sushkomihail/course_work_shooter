using System;

namespace SaveSystem.Settings
{
    [Serializable]
    public class SoundData
    {
        public float MusicVolume { get; }
        public float EffectsVolume { get; }

        public SoundData(float musicVolume, float effectsVolume)
        {
            MusicVolume = musicVolume;
            EffectsVolume = effectsVolume;
        }
    }
}