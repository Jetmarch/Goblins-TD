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
        [SerializeField] private TowerConfig _towerConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private EnemySensor _enemySensor;
        [SerializeField] private TowerView _towerView;
        [SerializeField] private WeaponView _weaponView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            var towerData = _towerConfig.GetPrototype();
            
            ConfigureSensor(builder, towerData);
            ConfigureTower(builder, towerData);
            ConfigureWeapon(builder);
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