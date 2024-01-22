using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class ScoreUi
    {
        private readonly TextMeshProUGUI _text;
        private readonly Animation _animation;

        private int _score;
        
        public ScoreUi(TextMeshProUGUI text, Animation animation)
        {
            _text = text;
            _animation = animation;
            
            _score = 0;
            _text.text = _score.ToString();
        }

        public void Update()
        {
            if (_text == null) return;
            
            _score += 1;
            _text.text = _score.ToString();

            if (_animation == null) return;
            
            if (_animation.isPlaying)
            {
                _animation.Stop();
                _animation.Rewind();
            }
            
            _animation.Play();
        }
    }
}