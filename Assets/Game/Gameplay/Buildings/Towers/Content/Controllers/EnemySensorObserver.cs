using System;
using VContainer.Unity;

namespace Game.Gameplay.Buildings.Controllers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class EnemySensorObserver : IInitializable, IDisposable
    {
        private readonly EnemySensor _enemySensor;
        private readonly TowerView _towerView;
        
        public EnemySensorObserver(EnemySensor enemySensor, TowerView towerView)
        {
            _enemySensor = enemySensor;
            _towerView = towerView;
        }

        public void Initialize()
        {
            _enemySensor.EnemyDetected += _towerView.SetTarget;
            _enemySensor.EnemyLost += _towerView.TargetLost;
        }

        public void Dispose()
        {
            _enemySensor.EnemyDetected -= _towerView.SetTarget;
            _enemySensor.EnemyLost -= _towerView.TargetLost;
        }
    }
}