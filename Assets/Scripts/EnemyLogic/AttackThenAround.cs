using System;
using UnityEngine;

namespace EnemyLogic
{
    public class AttackThenAround : MonoBehaviour
    {
        public float AttackCooldown;
        public float AttackDamage;
        public float AttackDistance;
        
        private Health _target;
        private float _elapsedTime;
        
        public void Construct(Health target)
        {
            _target = target;
        }

        private void Update()
        {
            var distance = Vector3.Distance(transform.position, _target.transform.position);

            if (distance <= AttackDistance)
            {
                TryAttack();
            }
        }

        private void TryAttack()
        {
            if (_elapsedTime >= AttackCooldown)
            {
                Attack();
                _elapsedTime = 0f;
            }
            else
            {
                _elapsedTime += Time.deltaTime;
            }
        }

        private void Attack() => 
            _target.ApplyDamage(AttackDamage);
    }
}