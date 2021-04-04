using System;
using System.Collections;
using UnityEngine;

namespace Building
{
    public class Rebuilding : MonoBehaviour
    {
        [SerializeField] 
        private float _time;

        [SerializeField] 
        private Vector3 _rebuildResult;

        [SerializeField] 
        private Transform _buildingModel;

        [SerializeField] 
        private ParticleSystem _buildingEffect;

        private bool _rebuildingNow;

        public bool Rebuild => _rebuildingNow;

        public event Action Undergrounded;
        
        public void StartRebuild()
        {
            if (_rebuildingNow)
                return;

            _rebuildingNow = true;
            StartCoroutine(Process());
        }

        private IEnumerator Process()
        {
            var timeStep = Time.fixedDeltaTime / _time;
            var t = 0f;
            var start = _buildingModel.localPosition;
            
            _buildingEffect.gameObject.SetActive(true);
            _buildingEffect.Play();

            while (t < 1)
            {
                _buildingModel.localPosition = Vector3.Lerp(start, _rebuildResult, t);
                t += timeStep;
                yield return new WaitForFixedUpdate();
            }
            
            Undergrounded?.Invoke();
            t = 0f;
            while (t < 1)
            {
                _buildingModel.localPosition = Vector3.Lerp(_rebuildResult, start, t);
                t += timeStep;
                yield return new WaitForFixedUpdate();
            }
            
            _buildingModel.localPosition = start;
            _rebuildingNow = false;
            _buildingEffect.Stop();
            _buildingEffect.gameObject.SetActive(false);
        }
    }
}