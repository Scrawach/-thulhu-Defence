using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgressView : MonoBehaviour
    {
        public Slider Slider;
        public float Smooth;

        private Score _score;
        private int _target;
        
        private float _targetValue;
        private float _smoothValue;
        
        public void Construct(Score score)
        {
            if (_score != null)
                _score.Changed -= OnScoreChanged;

            _score = score;
            _target = score.TargetValue;
            _score.Changed += OnScoreChanged;
            OnScoreChanged(_score.Value);
        }

        private void OnScoreChanged(int value)
        {
            _targetValue = value / (float)_target;
            _smoothValue = 0;
        }

        private void Update()
        {
            _smoothValue += Smooth * Time.deltaTime;
            _smoothValue = Mathf.Clamp(_smoothValue, 0f, 1f);
            Slider.value = Mathf.Lerp(Slider.value, _targetValue, _smoothValue);
        }
    }
}