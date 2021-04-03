using System;
using UnityEngine;

namespace EnemyLogic
{
    public class Health : MonoBehaviour
    {
        [SerializeField] 
        private float _initValue;

        [SerializeField] 
        private float _currentValue;

        public float Value { get; private set; }
        public float Limit { get; private set; }

        public event Action<float, float> Changed;
        public event Action Died;

        private void Awake() => 
            Construct(_initValue, _initValue);

        public void Construct(float current, float max)
        {
            Value = current;
            Limit = max;
        }

        public void ApplyDamage(float damage)
        {
            if (Value <= 0)
                return;
            
            Value -= damage;
            _currentValue = Value;
            Changed?.Invoke(Value, Limit);
            
            if (Value <= 0)
            {
                Died?.Invoke();
                return;
            }
        }
    }
}