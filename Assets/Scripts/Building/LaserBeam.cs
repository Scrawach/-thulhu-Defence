using System;
using UnityEngine;

namespace Building
{
    public class LaserBeam : MonoBehaviour
    {
        public LineRenderer LineRenderer = default;
        public GameObject Laser;

        public MeshRenderer MeshRenderer;
        public Material HighlightMaterial;

        private Material _originalMaterial;
        private bool _isActivated;
        
        private void Awake()
        {
            _originalMaterial = MeshRenderer.material;
        }

        public void Shoot(Vector3 point)
        {
            LineRenderer.SetPosition(1, point);
        }

        public void Deactivate()
        {
            if (_isActivated == false)
                return;
            
            _isActivated = false;
            Laser.SetActive(false);
            MeshRenderer.material = _originalMaterial;
        }

        public void Activate()
        {
            if (_isActivated)
                return;
            
            _isActivated = true;
            Laser.SetActive(true);
            MeshRenderer.material = HighlightMaterial;
        }
    }
}