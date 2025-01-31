﻿using System;
using System.Collections;
using Cthulhu;
using Infrastructure.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyLogic.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] 
        private float _distance = 5f;

        [SerializeField] 
        private float _height = 3f;

        [SerializeField] 
        private Scenario[] _scenarios;
        
        private GameFactory _gameFactory;
        private Scenario _current;
        private int _scenarioIndex;
        
        private bool _gameOver;
        private Coroutine _spawning;

        public void Construct(GameFactory gameFactory, Health homeHealth, Rise homeRise)
        {
            _gameFactory = gameFactory;

            homeHealth.Died += OnSpawnEnd;
            homeRise.Win += OnSpawnEnd;
        }

        private void OnSpawnEnd()
        {
            _gameOver = true;
            StopCoroutine(_spawning);
            _spawning = null;
        }

        private void Start() => 
            _spawning = StartCoroutine(Spawning());

        private IEnumerator Spawning()
        {
            yield return new WaitForSeconds(3f);
            _scenarioIndex = 0;

            while (_gameOver == false)
            {
                _current = _scenarios[_scenarioIndex];
                var submarineChance = _current.ChanceSubmarine;
                var rocketChance = _current.ChanceRocket;
                var timeOnScenario = _current.TimeOnThisScenario;
                var timeCooldown = _current.SpawnCooldown;
                var timeElapsed = 0f;

                while (timeElapsed < timeOnScenario)
                {
                    var randomValue = Random.value;

                    if (randomValue < rocketChance)
                    {
                        SpawnRocket();
                    }
                    else if (randomValue < rocketChance + submarineChance)
                    {
                        SpawnSubmarine();
                    }

                    yield return new WaitForSeconds(timeCooldown);
                    timeElapsed += timeCooldown;
                }

                _scenarioIndex++;

                if (_scenarios.Length <= _scenarioIndex)
                {
                    _scenarioIndex--;
                }
            }
        }

        private void SpawnRocket()
        {
            float angle = Random.Range(0, 360);
            var positionXY = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * _distance;
            var position = new Vector3(positionXY.x, _height, positionXY.y) + _gameFactory.Home.transform.position;
            
            _gameFactory.CreateRocket(position);
        }

        private void SpawnSubmarine()
        {
            float angle = Random.Range(0, 360);
            var positionXY = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * _distance;
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