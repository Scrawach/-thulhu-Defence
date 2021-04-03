using System;
using EnemyLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthViewSlider : MonoBehaviour
    {
        public Slider Slider;
        public float Smooth;

        private Health _health;
        private float _targetValue;
        private float _smoothValue;
        
        public void Construct(Health health)
        {
            if (_health != null)
                _health.Changed -= OnHealthChanged;

            _health = health;
            _health.Changed += OnHealthChanged;
            OnHealthChanged(_health.Value, _health.Limit);
        }

        private void Update()
        {
            _smoothValue += Smooth * Time.deltaTime;
            _smoothValue = Mathf.Clamp(_smoothValue, 0f, 1f);
            Slider.value = Mathf.Lerp(Slider.value, _targetValue, _smoothValue);
        }

        private void OnHealthChanged(float arg1, float arg2)
        {
            _targetValue = arg1 / arg2;
            _smoothValue = 0;
        }
    }
}