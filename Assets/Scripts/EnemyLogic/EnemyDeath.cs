using System;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _effect;
        
        private Health _health;

        public event Action Happened;

        private void Awake() => 
            _health = GetComponent<Health>();

        private void OnEnable() => 
            _health.Died += OnDied;

        private void OnDisable() => 
            _health.Died -= OnDied;

        private void OnDied()
        {
            Instantiate(_effect, transform.position, Quaternion.identity);
            Happened?.Invoke();
            Destroy(gameObject);
        }
    }
}