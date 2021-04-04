using System;
using UnityEngine;

namespace EnemyLogic.Spawner
{
    [Serializable]
    public class Scenario
    {
        public float ChanceSubmarine;
        
        public float ChanceRocket;

        public float SpawnCooldown;
        public float TimeOnThisScenario;
        
        public float LessCooldownAfter;
    }
}