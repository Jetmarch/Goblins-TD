using System.Collections.Generic;
using Game.GameEngine.Common;
using Game.Gameplay.Buildings.Controllers;
using Game.Gameplay.Projectiles;
using Game.Gameplay.Weapons;
using Modules.Core.GameLoop;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay.Buildings
{
    public class ProtocolHammerInstaller : LifetimeScope
    {
        [SerializeField] private GameLoopManager _gameLoopManager;
        [SerializeField] private RaycastProjectileFactory _raycastProjectileFactory;
        
        
        [SerializeField] private TowerConfig _towerConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private EnemySensor _enemySensor;
        [SerializeField] private TowerView _towerView;
        [SerializeField] private WeaponView _weaponView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            //Temp. Delete it later
            builder.RegisterInstance(_gameLoopManager).AsImplementedInterfaces();
            
            
            ConfigureProjectiles(builder);
            
            var towerData = _towerConfig.GetPrototype();
            
            ConfigureSensor(builder, towerData);
            ConfigureTower(builder, towerData);
            ConfigureWeapon(builder);
            ConfigureControllers(builder);
            
            builder.Register<RotatingTowerPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<WeaponPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        }

        private void ConfigureProjectiles(IContainerBuilder builder)
        {
            builder.Register<ProjectileRepository>(Lifetime.Scoped).AsImplementedInterfaces();
            
            var projectileFactories = new Dictionary<string, IProjectileFactory>();
            projectileFactories["Raycast"] = _raycastProjectileFactory;
            builder.RegisterInstance(projectileFactories);
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

        private void ConfigureWeapon(IContainerBuilder builder)
        {
            var weaponData = _weaponConfig.GetPrototype();
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