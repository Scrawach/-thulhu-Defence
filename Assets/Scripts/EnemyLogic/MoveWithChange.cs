using EnemyLogic.BulletLogic;
using UnityEngine;

namespace EnemyLogic
{
    public class MoveWithChange : MonoBehaviour
    {
        [SerializeField] 
        private float _speed;

        [SerializeField] 
        private float _distance;

        [SerializeField] 
        private Bullet _bullet;
        
        [SerializeField] 
        private RotateToTarget _rotate;

        private Transform _target;
        private bool _moving;
        
        public void Construct(Transform target)
        {
            _target = target;
            _moving = true;
            _bullet.enabled = false;
        }

        private void Update()
        {
            if (_moving == false)
                return;

            if (Vector3.Distance(transform.position, _target.position) < _distance)
            {
                _moving = true;
                _bullet.enabled = true;
                _rotate.Unlock();
                enabled = false;
            }
            
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