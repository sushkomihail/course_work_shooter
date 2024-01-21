using SaveSystem.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public class ControlSettings : MonoBehaviour
    {
        [SerializeField] private Slider _verticalSensitivitySlider;
        [SerializeField] private Slider _horizontalSensitivitySlider;

        public ControlData Data => new ControlData(
            _verticalSensitivitySlider.value,
            _horizontalSensitivitySlider.value
            );

        public void Initialize(ControlData controlData)
        {
            if (controlData == null) return;
            
            _verticalSensitivitySlider.value = controlData.VerticalSensitivity;
            _horizontalSensitivitySlider.value = controlData.HorizontalSensitivity;
        }
    }
}