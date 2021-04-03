using System;
using System.Collections;
using EnemyLogic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthTextView : MonoBehaviour
    {
        public Health Health;
        public TextMeshProUGUI View;
        public CanvasFade Fade;

        private Coroutine _coroutine;
        
        private void OnEnable() => 
            Health.Changed += OnHealthChanged;

        private void OnDisable() => 
            Health.Changed -= OnHealthChanged;

        private void OnHealthChanged(float arg1, float arg2)
        {
            View.text = arg1.ToString();
            Fade.Show();

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            
            _coroutine = StartCoroutine(Waiting());
        }

        private IEnumerator Waiting()
        {
            yield return new WaitForSeconds(5f);
            Fade.Hide();
        }
    }
}