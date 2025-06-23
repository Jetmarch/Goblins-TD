using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameEngine.Pipeline;
using Game.Gameplay.Storages;
using Game.Gameplay.Towers.Components;
using Modules.Core.GameLoop;

namespace Game.Gameplay.Towers.Presenters
{
    public sealed class RotatingTowerPresenter : ITowerPresenter, IUpdateListener
    {
        public event Action AttackRequest;
        public ITarget Target => _currentTarget;
        public ITowerView View => _view;
        public TowerData TowerData => _towerData;
        public BulletStorage BulletStorage => _bulletStorage;
        private readonly ITowerView _view;
        private readonly TowerData _towerData;
        
        private ITarget _currentTarget;
        private BulletStorage _bulletStorage;

        public RotatingTowerPresenter(ITowerView view, TowerData towerData, BulletStorage bulletStorage)
        {
            _view = view;
            _towerData = towerData;
            _bulletStorage = bulletStorage;
        }
        
        public void SetTarget(ITarget target)
        {
            _currentTarget = target;
        }

        public void LostTarget()
        {
            _currentTarget = null;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_currentTarget == null) return;
            
            var weaponRotation = Rotator.SmoothRotateTowardsTarget(_currentTarget.Transform.position,
                _view.WeaponTransform.position, _view.WeaponTransform.rotation, _towerData.RotateSpeed, deltaTime);
            _view.WeaponTransform.rotation = weaponRotation;
            if (!ConeDetector.IsTargetInCone(_currentTarget.Transform.position,
                    _view.WeaponTransform.position,
                    _view.WeaponTransform.up, _towerData.AttackAngle)) return;

            if (_bulletStorage.CurrentAmount < _towerData.AmountOfBulletsPerShot) return;
            
            _bulletStorage.Decrease(_towerData.AmountOfBulletsPerShot);
            AttackRequest?.Invoke();
        }
    }
}