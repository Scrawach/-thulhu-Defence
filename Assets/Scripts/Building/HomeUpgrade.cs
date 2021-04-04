using System;
using EnemyLogic;
using UnityEngine;

namespace Building
{
    public class HomeUpgrade : MonoBehaviour, IUpgradable
    {
        private BuildingDescription _buildingDescription;
        private Health _health;

        private void Awake()
        {
            _buildingDescription = GetComponent<BuildingDescription>();
            _health = GetComponent<Health>();
            Price = _buildingDescription.Description.UpgradePrice;
        }

        private void OnEnable() => 
            _health.Changed += OnHealthChanged;

        private void OnDisable() => 
            _health.Changed -= OnHealthChanged;

        private void OnHealthChanged(float arg1, float arg2)
        {
            _buildingDescription.Description.Value = arg1;
            _buildingDescription.Description.Update();
        }

        public void Upgrade()
        {
            var description = _buildingDescription.Description;
            _health.ApplyDamage(-1);
            description.Value = _health.Value;
            description.Update();
        }

        public int Price { get; set; }
    }
}