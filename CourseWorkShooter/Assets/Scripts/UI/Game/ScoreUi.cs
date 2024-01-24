using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class ScoreUi
    {
        private readonly TextMeshProUGUI _text;
        private readonly Animation _animation;

        public int Score { get; private set; }
        
        public ScoreUi(TextMeshProUGUI text, Animation animation)
        {
            _text = text;
            _animation = animation;
            
            Score = 0;
            _text.text = Score.ToString();
        }

        public void Update()
        {
            if (_text == null) return;
            
            Score += 1;
            _text.text = Score.ToString();

            if (_animation == null) return;
            
            if (_animation.isPlaying)
            {
                _animation.Stop();
            }
            
            _animation.Play();
        }
    }
}