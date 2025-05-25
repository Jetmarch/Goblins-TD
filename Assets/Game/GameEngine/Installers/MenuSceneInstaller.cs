using Game.Gameplay.Levels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.GameEngine.Installers
{
    public sealed class MenuSceneInstaller : LifetimeScope
    {
        [SerializeField] private LevelManager _levelManager; 
        
        protected override void Configure(IContainerBuilder builder)
        {
            // builder.RegisterInstance(_levelManager).AsImplementedInterfaces();
        }
    }
}