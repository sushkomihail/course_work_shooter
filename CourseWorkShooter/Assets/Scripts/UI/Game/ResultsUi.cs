using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class ResultsUi
    {
        private readonly GameObject _resultsWindow;
        private readonly TextMeshProUGUI _scoreText;
        private readonly TextMeshProUGUI _timeText;

        public ResultsUi(GameObject resultsWindow, TextMeshProUGUI scoreText, TextMeshProUGUI timeText)
        {
            _resultsWindow = resultsWindow;
            _scoreText = scoreText;
            _timeText = timeText;
        }

        public void Update(string score, string time)
        {
            _resultsWindow.SetActive(true);
            _scoreText.text = score;
            _timeText.text = time;
        }
    }
}