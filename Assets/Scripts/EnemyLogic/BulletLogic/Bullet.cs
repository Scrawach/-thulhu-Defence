using System;
using Cthulhu;
using UnityEngine;

namespace EnemyLogic.BulletLogic
{
    [RequireComponent(typeof(Health))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] 
        private int _damage;

        [SerializeField] 
        private float _speed;
        
        private Health _target;
        private Health _bulletHealth;

        private void Awake()
        {
            _bulletHealth = GetComponent<Health>();
        }

        public void Construct(Health target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target == null)
                return;
            
            Move();
        }

        private void Move()
        {
            var direction = _target.transform.position - transform.position;
            direction.Normalize();
            
            var step = _speed * Time.deltaTime;
            var movement = direction * step;

            transform.position += movement;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Rise rise))
            {
                rise.GetComponent<Health>().ApplyDamage(_damage);
                _bulletHealth.ApplyDamage(10000);
            }
        }
    }
}
