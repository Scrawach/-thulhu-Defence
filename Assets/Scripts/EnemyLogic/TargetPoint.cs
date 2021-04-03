using System;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(Health))]
    public class TargetPoint : MonoBehaviour
    {
        public Health Health { get; private set; }
        public Vector3 Position => transform.position;

        private void Awake()
        {
            Health = GetComponent<Health>();
        }
    }
}