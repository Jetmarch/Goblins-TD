using Game.GameEngine.EntityComponents;
using Game.Gameplay.Towers.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay.Towers.PlayerBase
{
    public sealed class PlayerBaseInstaller : LifetimeScope
    {
        [SerializeField] private HealthStorage _healthStorage;
        [SerializeField] private EnemySensor _sensor;
        [SerializeField] private EnemySensorData _sensorData;

        [SerializeField] private ComponentStorage _componentStorage;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_healthStorage).AsSelf().AsImplementedInterfaces();
            builder.RegisterInstance(_sensor).AsImplementedInterfaces();
            builder.RegisterInstance(_sensorData);
            builder.RegisterInstance(_componentStorage).AsImplementedInterfaces();

            builder.Register<PlayerBaseHealthController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerBaseDestroyController>(Lifetime.Scoped).AsImplementedInterfaces();
            
            _componentStorage.AddHealthStorage(_healthStorage);
        }
    }
}