using SaveSystem;
using UnityEngine;

namespace UI
{
    public class OptionsLoader : MonoBehaviour
    {
        [SerializeField] private ControlSettings _controlSettings;
        
        private void OnEnable()
        {
            ControlData controlData = Saver<ControlData>.Load(DataTypes.Control);
            _controlSettings.Initialize(controlData);
        }
    }
}