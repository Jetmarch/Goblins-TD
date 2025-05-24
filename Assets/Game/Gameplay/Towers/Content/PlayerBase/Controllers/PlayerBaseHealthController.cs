using System;
using Game.GameEngine;
using Game.GameEngine.EntityComponents;
using VContainer.Unity;

namespace Game.Gameplay.Towers.PlayerBase
{
    internal sealed class PlayerBaseHealthController : IInitializable, IDisposable
    {
        private readonly HealthStorage _healthStorage;
        private readonly ITargetSensor _targetSensor;

        public PlayerBaseHealthController(HealthStorage healthStorage, ITargetSensor targetSensor)
        {
            _healthStorage = healthStorage;
            _targetSensor = targetSensor;
        }

        public void Initialize()
        {
            _targetSensor.EnemyDetected += TargetSensorOnEnemyDetected;
        }


        public void Dispose()
        {
            _targetSensor.EnemyDetected -= TargetSensorOnEnemyDetected;
        }
        
        private void TargetSensorOnEnemyDetected(ITarget obj)
        {
            _healthStorage.DecreaseHealth(1);
        }
    }
}