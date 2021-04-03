using System;
using UnityEngine;

namespace EnemyLogic
{
    public class MoveToTarget : MonoBehaviour
    {
        [SerializeField] 
        private float _speed;

        private Transform _target;
        
        public void Construct(Transform target) => 
            _target = target;

        private void Update()
        {
            if (_target == null)
                return;
            
            Move();
        }

        private void Move()
        {
            var direction = _target.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            
            var step = _speed * Time.deltaTime;
            var movement = direction * step;

            transform.position += movement;
        }
    }
}