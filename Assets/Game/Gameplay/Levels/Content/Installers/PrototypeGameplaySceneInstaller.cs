using Game.Gameplay.Projectiles;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay.Levels.Content
{
    public sealed class PrototypeGameplaySceneInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureProjectiles(builder);
        }

        private void ConfigureProjectiles(IContainerBuilder builder)
        {
            builder.Register<ProjectileRepository>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}