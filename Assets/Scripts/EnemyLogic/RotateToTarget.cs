using UnityEngine;

namespace EnemyLogic
{
    public class RotateToTarget : MonoBehaviour
    {
        private Transform _target;
        
        public void Construct(Transform target)
        {
            _target = target;
            var direction = _target.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }
}