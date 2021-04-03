using System;
using Infrastructure.Factory;
using Items;
using UnityEngine;

namespace Building
{
    public class Temple : MonoBehaviour
    {
        [SerializeField] 
        private float _timeCooldown;

        [SerializeField]
        private Transform _spawnPosition;

        private GameFactory _gameFactory;
        private float _timeElapsed;

        public void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Update()
        {
            if (_timeElapsed >= _timeCooldown)
            {
                _gameFactory.CreateDrop(DropType.Bonus, _spawnPosition.position);
                _timeElapsed = 0f;
            }
            else
            {
                _timeElapsed += Time.deltaTime;
            }
        }
    }
}