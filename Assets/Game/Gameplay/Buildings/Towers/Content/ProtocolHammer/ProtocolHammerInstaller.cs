using Game.Gameplay.Buildings.Controllers;
using Game.Gameplay.Weapons;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay.Buildings
{
    public class ProtocolHammerInstaller : LifetimeScope
    {
        [SerializeField] private TowerConfig _config;
        [SerializeField] private TowerData _towerData;

        [SerializeField] private EnemySensor _enemySensor;
        [SerializeField] private TowerView _towerView;
        [SerializeField] private WeaponView _weaponView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            var towerData = _config.GetPrototype();
            builder.RegisterInstance(towerData);
            
            var weaponData = new WeaponData(towerData.ProjectileId, towerData.AttackSpeed);
            builder.RegisterInstance(weaponData);

            var enemySensorData = new EnemySensorData(towerData.AttackRadius);
            builder.RegisterInstance(enemySensorData);
            
            builder.RegisterInstance(_enemySensor);
            builder.RegisterInstance(_towerView);
            builder.RegisterInstance(_weaponView);
            
            builder.Register<EnemySensorObserver>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<AttackRequestObserver>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}