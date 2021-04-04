using System;
using Cthulhu;
using UnityEngine;

namespace EnemyLogic.BulletLogic
{
    public class SelfDestruction : MonoBehaviour
    {
        [SerializeField] 
        private int _damage;
        
        private Health _bulletHealth;

        private void Awake() => 
            _bulletHealth = GetComponent<Health>();

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
