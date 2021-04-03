using System;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(Health))]
    public class DamageHighlight : MonoBehaviour
    {
        public MeshRenderer MeshRenderer;
        public Material HighlightMaterial;

        private Material _originalMaterial;
        private Health _health;
        private bool _isDamaged;

        private void Awake() => 
            _health = GetComponent<Health>();

        private void OnEnable() => 
            _health.Changed += OnHealthChanged;

        private void OnDisable() => 
            _health.Changed -= OnHealthChanged;

        private void OnHealthChanged(float arg1, float arg2) => 
            Enable();

        public void Enable()
        {
            if (_isDamaged)
                return;

            _isDamaged = true;
            MeshRenderer.material = HighlightMaterial;
        }
    }
}