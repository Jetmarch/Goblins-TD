using System;
using Game.Gameplay.Impacts;
using Game.Gameplay.Towers;
using Modules.Core.GameLoop;

namespace Game.Gameplay.Weapons
{
    internal sealed class WeaponPresenter : IWeaponPresenter, IUpdateListener
    {
        public event Action<SpawnProjectileData, ImpactHitData> ProjectileRequest;
        
        private readonly IWeaponView _view;
        private readonly WeaponData _weaponData;

        private float _currentAttackDelay;
        private ITarget _currentTarget;
        

        public WeaponPresenter(WeaponData weaponData, IWeaponView view)
        {
            _weaponData = weaponData;
            _view = view;
        }

        public void Attack()
        {
            if (_currentAttackDelay > 0f)
            {
                return;
            }

            var spawnProjectileData = new SpawnProjectileData(_weaponData.ProjectileId,
                _view.ShootPointTransform.position, _view.ShootPointTransform.rotation);
            var impactHitData = new ImpactHitData(_weaponData.Damage, _weaponData.Knockback);
            ProjectileRequest?.Invoke(spawnProjectileData, impactHitData);
            
            _view.PlayAttackAnimation();

            _currentAttackDelay = 1 / _weaponData.AttackSpeed;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_currentAttackDelay > 0f)
            {
                _currentAttackDelay -= deltaTime;
            }
        }
    }
}