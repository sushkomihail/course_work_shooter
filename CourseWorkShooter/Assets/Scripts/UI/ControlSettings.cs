using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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
            if (controlData != null)
            {
                _verticalSensitivitySlider.value = controlData.VerticalSensitivity;
                _horizontalSensitivitySlider.value = controlData.HorizontalSensitivity;
            }
        }
    }
}