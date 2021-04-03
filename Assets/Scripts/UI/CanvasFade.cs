using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasFade : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 1f)] 
        private float _time;
        
        private CanvasGroup _canvasGroup;
        private Coroutine _fading;

        public UnityEvent EndInFaded;
        public UnityEvent EndOutFaded;
        
        private void Awake() => 
            _canvasGroup = GetComponent<CanvasGroup>();

        public void Show()
        {
            ResetCoroutine();
            _fading = StartCoroutine(FadeOut());
        }

        public void Hide()
        {
            ResetCoroutine();
            _fading = StartCoroutine(FadeIn());
        }

        private void ResetCoroutine()
        {
            if (IsFading())
            {
                StopCoroutine(_fading);
                _fading = null;
            }
        }

        private bool IsFading() =>
            _fading != null;

        private IEnumerator FadeIn()
        {
            var step = Time.fixedDeltaTime / _time;
            while (_canvasGroup.alpha > 0f)
            {
                _canvasGroup.alpha -= step;
                yield return new WaitForFixedUpdate();
            }
            
            EndInFaded?.Invoke();
            _canvasGroup.blocksRaycasts = false;
        }

        private IEnumerator FadeOut()
        {
            var step = Time.fixedDeltaTime / _time;
            while (_canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += step;
                yield return new WaitForFixedUpdate();
            }
            
            EndOutFaded?.Invoke();
            _canvasGroup.blocksRaycasts = true;
        }

        public void SetAlpha(float value)
        {
            _canvasGroup.alpha = value;
        }
    }
}
