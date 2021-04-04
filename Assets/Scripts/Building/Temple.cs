using System;
using Board.Tile;
using Cthulhu;
using EnemyLogic;
using Infrastructure.Factory;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Building
{
    public class Temple : MonoBehaviour, IUpgradable
    {
        [SerializeField] 
        private float _timeCooldown;

        [SerializeField]
        private Transform _spawnPosition;

        [SerializeField] 
        private float _moneyChance;

        private Description _description;

        private GameFactory _gameFactory;
        private float _timeElapsed;
        
        private Rebuilding _rebuilding;
        private Health _eggHealth;
        private Rise _homeRise;
        private bool _lock;

        private void Awake()
        {
            _rebuilding = GetComponent<Rebuilding>();
            _description = GetComponent<BuildingDescription>().Description;
            Price = _description.UpgradePrice;
        }

        public void Construct(GameFactory gameFactory, Health eggHealth, Rise homeRise)
        {
            if (_eggHealth != null)
            {
                _eggHealth.Died -= OnEggDied;
                _eggHealth = null;
            }

            if (_homeRise != null)
            {
                _homeRise.Win -= OnWin;
                _homeRise = null;
            }
            
            _gameFactory = gameFactory;

            _eggHealth = eggHealth;
            _homeRise = homeRise;
            
            _eggHealth.Died += OnEggDied;
            _homeRise.Win += OnWin;
        }

        private void OnWin()
        {
            _lock = true;
        }

        private void OnEggDied()
        {
            _lock = true;
        }

        private void Update()
        {
            if (_rebuilding.Rebuild || _lock)
                return;
            
            if (_timeElapsed >= _timeCooldown)
            {
                if (_moneyChance < Random.value)
                    _gameFactory.CreateDrop(DropType.Money, _spawnPosition.position);
                else
                    _gameFactory.CreateDrop(DropType.Bonus, _spawnPosition.position);
                
                _timeElapsed = 0f;
            }
            else
            {
                _timeElapsed += Time.deltaTime;
            }
        }

        public void Upgrade()
        {
            if (_description.CanUpgrade == false)
            {
                return;
            }
            
            _rebuilding.StartRebuild();
            _timeCooldown += _description.UpgradeBonus;

            if (_timeCooldown <= 1)
            {
                _description.CanUpgrade = false;
                _timeCooldown = 1;
            }

            _description.Value = _timeCooldown;
            _description.Update();
        }

        public int Price { get; set; }
    }
}