using System;
using VContainer.Unity;

namespace Game.Gameplay.Buildings.Controllers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class EnemySensorObserver : IInitializable, IDisposable
    {
        private readonly ITargetSensor _targetSensor;
        private readonly ITowerPresenter _towerPresenter;
        
        public EnemySensorObserver(EnemySensor targetSensor, ITowerPresenter towerPresenter)
        {
            _targetSensor = targetSensor;
            _towerPresenter = towerPresenter;
        }

        public void Initialize()
        {
            _targetSensor.EnemyDetected += _towerPresenter.SetTarget;
            _targetSensor.EnemyLost += LostTarget;
        }

        public void Dispose()
        {
            _targetSensor.EnemyDetected -= _towerPresenter.SetTarget;
            _targetSensor.EnemyLost -= LostTarget;
        }

        private void LostTarget(ITarget _)
        {
            _towerPresenter.LostTarget();
        }
    }
}