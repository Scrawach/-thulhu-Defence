using System;
using System.Collections;
using Infrastructure.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyLogic.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] 
        private float _spawnCooldownTime = 1f;

        [SerializeField] 
        private float _distance = 5f;

        [SerializeField] 
        private float _height = 3f;

        private GameFactory _gameFactory;
        private bool _gameOver;

        public void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _gameFactory.Home.GetComponent<Health>().Died += OnHomeDied;
        }

        private void OnHomeDied()
        {
            _gameOver = true;
        }

        private void Start()
        {
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            yield return new WaitForSeconds(1f);
            
            while (_gameOver == false)
            {
                yield return new WaitForSeconds(_spawnCooldownTime);
                SpawnRandom();
            }
        }
        
        private void SpawnRandom()
        {
            float Angle = Random.Range(0, 360);
            var positionXY = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)) * _distance;
            var position = new Vector3(positionXY.x, _height, positionXY.y) + _gameFactory.Home.transform.position;
            
            _gameFactory.CreateEnemy(position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            
            if (_gameFactory == null)
                Gizmos.DrawWireSphere(Vector3.zero, _distance);
            else
                Gizmos.DrawWireSphere(_gameFactory.Home.transform.position, _distance);
        }
    }
}