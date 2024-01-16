using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField] private ControlSettings controlSettings;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SaveSettings);
        }

        private void SaveSettings()
        {
            Saver<ControlData>.Save(controlSettings.Data, DataTypes.Control);
        }
    }
}