using System.Collections.Generic;
using Game.Gameplay.Projectiles;
using Modules.Core.GameLoop;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.GameEngine.Installers
{
    public sealed class GameplaySceneInstaller : LifetimeScope
    {
        [SerializeField] private GameLoopManager _gameLoopManager;
        [SerializeField] private LayerMask _raycastProjectilesLayerMask;
        
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureGameLoop(builder);
            ConfigureProjectiles(builder);
        }

        private void ConfigureGameLoop(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameLoopManager).AsImplementedInterfaces();
        }
        
        private void ConfigureProjectiles(IContainerBuilder builder)
        {
            builder.Register<ProjectileRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            
            //TODO: Move it to serialized fields
            var projectileFactories = new Dictionary<string, IProjectileFactory>();
            projectileFactories["Raycast"] = new RaycastProjectileFactory(_raycastProjectilesLayerMask);
            builder.RegisterInstance(projectileFactories);
        }
    }
}