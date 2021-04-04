using System;
using Infrastructure.Factory;
using UnityEngine;

namespace EnemyLogic
{
    public class AttackThenAround : MonoBehaviour
    {
        public float AttackCooldown;
        public float AttackDistance;
        public Transform ShootPosition;
        public MoveToTarget Mover;
        
        private Health _target;
        private GameFactory _gameFactory;
        private float _elapsedTime;

        public void Construct(Health target, GameFactory gameFactory)
        {
            _target = target;
            _gameFactory = gameFactory;

            _target.Died += OnTargetDied;
        }

        private void OnDestroy()
        {
            _target.Died -= OnTargetDied;
        }

        private void OnTargetDied()
        {
            if (Mover != null)
                Mover.enabled = false;
            
            enabled = false;
        }

        private void Update()
        {
            var distance = Vector3.Distance(transform.position, _target.transform.position);

            if (distance <= AttackDistance)
            {
                Mover.enabled = false;
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

        private void Attack()
        {
            _gameFactory.CreateBullet(ShootPosition.position);
        }
    }
}