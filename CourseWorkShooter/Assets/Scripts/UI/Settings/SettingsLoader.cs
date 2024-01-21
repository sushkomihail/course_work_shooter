using SaveSystem;
using SaveSystem.Settings;
using UnityEngine;

namespace UI.Settings
{
    public class SettingsLoader : MonoBehaviour
    {
        [SerializeField] private ControlSettings _controlSettings;
        [SerializeField] private SoundSettings _soundSettings;
        
        private void OnEnable()
        {
            ControlData controlData = Saver<ControlData>.Load(DataTypes.Control);
            SoundData soundData = Saver<SoundData>.Load(DataTypes.Sound);
            
            if (_controlSettings!= null) _controlSettings.Initialize(controlData);
            if (_controlSettings != null) _soundSettings.Initialize(soundData);
        }
    }
}