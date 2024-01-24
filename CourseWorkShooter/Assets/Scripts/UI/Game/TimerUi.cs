using PauseSystem;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class TimerUi
    {
        private readonly TextMeshProUGUI _timeText;

        private static float _time;

        public float Time => _time;

        public TimerUi(TextMeshProUGUI timeText)
        {
            _timeText = timeText;

            _time = 0;
        }

        public void Update()
        {
            if (_timeText == null) return;
            
            _time += UnityEngine.Time.deltaTime;
            _timeText.text = TimeConvertedToReadableFormat(_time);
        }

        public static string TimeConvertedToReadableFormat(float time)
        {
            float minutes = Mathf.FloorToInt(time / 60); 
            float seconds = Mathf.FloorToInt(time % 60);
            float milliSeconds = (time % 1) * 100;
            return $"{minutes:00}:{seconds:00}:{milliSeconds:00}";
        }
    }
}