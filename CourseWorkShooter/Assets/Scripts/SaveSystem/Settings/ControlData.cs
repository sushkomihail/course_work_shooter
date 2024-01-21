using System;

namespace SaveSystem.Settings
{
    [Serializable]
    public class ControlData
    {
        public float VerticalSensitivity { get; }
        public float HorizontalSensitivity { get; }

        public ControlData(float verticalSensitivity, float horizontalSensitivity)
        {
            VerticalSensitivity = verticalSensitivity;
            HorizontalSensitivity = horizontalSensitivity;
        }
    }
}