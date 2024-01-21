using SaveSystem;
using SaveSystem.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ControlSettings _controlSettings;
        [SerializeField] private SoundSettings _soundSettings;

        private void Awake()
        {
            if (_button != null) _button.onClick.AddListener(SaveSettings);
        }

        private void SaveSettings()
        {
            if (_controlSettings != null)
                Saver<ControlData>.Save(_controlSettings.Data, DataTypes.Control);
            
            if (_soundSettings != null)
                Saver<SoundData>.Save(_soundSettings.Data, DataTypes.Sound);
        }
    }
}