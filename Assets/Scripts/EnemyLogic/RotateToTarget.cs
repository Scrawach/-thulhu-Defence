using System;
using UnityEngine;

namespace EnemyLogic
{
    public class RotateToTarget : MonoBehaviour
    {
        [SerializeField] 
        private Vector3 _blockRotation = default;

        [SerializeField] 
        private float _speed = 45f;

        [SerializeField] 
        private bool _updating;
        
        private Transform _target;
        
        public void Construct(Transform target)
        {
            _target = target;

            var direction = _target.position - transform.position;
            direction.x *= _blockRotation.x;
            direction.y *= _blockRotation.y;
            direction.z *= _blockRotation.z;
            
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }

        public void Unlock()
        {
            _updating = true;
            _blockRotation = Vector3.one;
        }

        public void Update()
        {
            if (_updating == false)
                return;
            
            var direction = _target.position - transform.position;
            direction.x *= _blockRotation.x;
            direction.y *= _blockRotation.y;
            direction.z *= _blockRotation.z;
            
            var step = _speed * Time.deltaTime;
            var targetRot = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, step);
        }
    }
}