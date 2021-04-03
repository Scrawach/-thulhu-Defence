using System;
using Infrastructure.Factory;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyLogic
{
    [RequireComponent(typeof(EnemyDeath))]
    public class DeathDrop : MonoBehaviour
    {
        public DropType Type;
        public float Chance;
        
        private GameFactory _gameFactory;
        private EnemyDeath _death;
        
        public void Construct(GameFactory gameFactory) => 
            _gameFactory = gameFactory;

        private void Awake() => 
            _death = GetComponent<EnemyDeath>();

        private void OnEnable() => 
            _death.Happened += OnDeathHappened;

        private void OnDisable() => 
            _death.Happened -= OnDeathHappened;

        private void OnDeathHappened()
        {
            if (Random.value > Chance)
                return;
            
            _gameFactory.CreateDrop(Type, transform.position);
        }
    }
}