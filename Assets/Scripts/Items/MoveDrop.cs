using System;
using UnityEngine;

namespace Items
{
    public class MoveDrop : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _direction = Vector3.up;
        
        [SerializeField] 
        private float _speed = 1f;

        private void Update()
        {
            var step = _speed * Time.deltaTime;
            var movement = _direction * step;
            transform.position += movement;
        }
    }
}