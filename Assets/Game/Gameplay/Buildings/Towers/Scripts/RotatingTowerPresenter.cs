using System;
using Modules.Core.GameLoop;

namespace Game.Gameplay.Buildings
{
    internal sealed class RotatingTowerPresenter : ITowerPresenter, IUpdateListener
    {
        public event Action AttackRequest;
        
        private readonly ITowerView _view;
        private readonly TowerData _towerData;
        
        private ITarget _currentTarget;

        public RotatingTowerPresenter(ITowerView view, TowerData towerData)
        {
            _view = view;
            _towerData = towerData;
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
            
            var weaponRotation = Rotator.SmoothRotateTowardsTarget(_currentTarget.Transform.position, _view.WeaponTransform.position, _view.WeaponTransform.rotation, _towerData.RotateSpeed, deltaTime);
            _view.WeaponTransform.rotation = weaponRotation;

            if (ConeDetector.IsTargetInCone(_currentTarget.Transform.position, _view.WeaponTransform.position, _view.WeaponTransform.up, _towerData.AttackAngle))
            {
                AttackRequest?.Invoke();
                _view.PlayAttackFX();
            }
        }
    }
}