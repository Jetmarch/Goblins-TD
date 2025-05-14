using Game.GameEngine;
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
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_healthStorage);
            builder.RegisterInstance(_sensor).AsImplementedInterfaces();
            builder.RegisterInstance(_sensorData);

            builder.Register<PlayerBaseHealthController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerBaseDestroyController>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}