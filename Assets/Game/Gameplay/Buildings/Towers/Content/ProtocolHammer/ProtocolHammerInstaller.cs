using Game.GameEngine.Common;
using Game.Gameplay.Buildings.Controllers;
using Game.Gameplay.Projectiles;
using Game.Gameplay.Weapons;
using Modules.Core.GameLoop;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay.Buildings
{
    public class ProtocolHammerInstaller : LifetimeScope
    {
        [SerializeField] private GameLoopManager _gameLoopManager;
        
        [SerializeField] private ProjectileFactory _projectileFactory;
        
        [SerializeField] private TowerConfig _config;
        [SerializeField] private EnemySensor _enemySensor;
        [SerializeField] private TowerView _towerView;
        [SerializeField] private WeaponView _weaponView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            //Temp. Delete it later
            builder.RegisterInstance(_gameLoopManager).AsImplementedInterfaces();
            builder.RegisterInstance(_projectileFactory).AsImplementedInterfaces();
            
            var towerData = _config.GetPrototype();
            
            ConfigureSensor(builder, towerData);
            ConfigureTower(builder, towerData);
            ConfigureWeapon(builder, towerData);
            ConfigureControllers(builder);
            
            builder.Register<RotatingTowerPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<WeaponPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        }
        
        private void ConfigureSensor(IContainerBuilder builder, TowerData towerData)
        {
            var enemySensorData = new EnemySensorData(towerData.AttackRadius);
            builder.RegisterInstance(enemySensorData);
            
            builder.RegisterInstance(_enemySensor).AsImplementedInterfaces();
        }

        private void ConfigureTower(IContainerBuilder builder, TowerData towerData)
        {
            builder.RegisterInstance(towerData);
            builder.RegisterInstance(_towerView).AsImplementedInterfaces();
        }

        private void ConfigureWeapon(IContainerBuilder builder, TowerData towerData)
        {
            var weaponData = new WeaponData(towerData.ProjectileId, towerData.AttackSpeed);
            builder.RegisterInstance(weaponData);
            
            builder.RegisterInstance(_weaponView).AsImplementedInterfaces();
        }

        private void ConfigureControllers(IContainerBuilder builder)
        {
            builder.Register<EnemySensorObserver>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<AttackRequestObserver>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ProjectileRequestObserver>(Lifetime.Scoped).AsImplementedInterfaces();
            
            builder.Register<GameLoopController>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}