using System;
using System.Collections;
using UnityEngine;

namespace Building
{
    public class Building : MonoBehaviour
    {
        [SerializeField] 
        private float _time;

        [SerializeField] 
        private Vector3 _buildResult;

        [SerializeField] 
        private Transform _buildingModel;

        [SerializeField] 
        private ParticleSystem _buildingEffect;
        
        private void Awake()
        {
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
                _buildingModel.localPosition = Vector3.Lerp(start, _buildResult, t);
                t += timeStep;
                yield return new WaitForFixedUpdate();
            }
            
            _buildingModel.localPosition = _buildResult;
            _buildingEffect.gameObject.SetActive(false);
            _buildingEffect.Stop();
        }
        
    }
}