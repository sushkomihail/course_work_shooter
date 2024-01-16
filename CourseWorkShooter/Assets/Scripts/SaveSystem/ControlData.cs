using System;

namespace SaveSystem
{
    [Serializable]
    public class ControlData : ISaveable
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