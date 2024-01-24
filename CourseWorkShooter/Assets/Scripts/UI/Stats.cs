using SaveSystem;
using TMPro;
using UI.Game;
using UnityEngine;

namespace UI
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _timeText;

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            StatsData data = Saver<StatsData>.Load(DataTypes.Stats);

            if (data == null) return;

            if (_scoreText != null) _scoreText.text = data.Score.ToString();
            if (_timeText != null) _timeText.text = TimerUi.TimeConvertedToReadableFormat(data.Time);
        }
    }
}