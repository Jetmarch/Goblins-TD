using System.Collections.Generic;
using Game.Gameplay.Controllers;
using Game.Gameplay.Enemies;
using Game.Gameplay.Levels;
using Game.Gameplay.Projectiles;
using Game.Gameplay.Towers.PlayerBase;
using Game.Gameplay.WaveSystem;
using Game.Meta.PlayerStash;
using Game.Meta.Rewards;
using Game.UI.GameResult;
using Game.UI.PlayerBaseInfo;
using Modules.Core.GameLoop;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.GameEngine.Installers
{
    public sealed class GameplaySceneInstaller : LifetimeScope
    {
        [Header("Game Loop")] 
        [SerializeField] private GameLoopManager _gameLoopManager;

        [Header("Waves")]
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private WaveManager _waveManager;
        
        [Header("Enemies")]
        [SerializeField] private EnemyManager _enemyManager;

        [Header("Player")]
        [SerializeField] private GameObject _playerBasePrefab;
        [SerializeField] private Transform _playerBaseParent;


        [Header("UI")]
        [SerializeField] private PlayerBaseInfoView _playerBaseInfoView;
        [SerializeField] private GameResultView _gameResultView;
        
        [Header("Projectiles")]
        [SerializeField] private LayerMask _raycastProjectilesLayerMask;
        
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureLevelData(builder);
            ConfigureGameLoop(builder);
            ConfigureGameplayManager(builder);
            ConfigureProjectiles(builder);
            ConfigureWaves(builder);
            ConfigureControllers(builder);
            ConfigurePlayerBase(builder);
            ConfigureEnemies(builder);
            ConfigureUI(builder);
            
            
            builder.Register<RewardManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerStash>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance(_gameResultView).AsSelf();
        }

        private void ConfigureLevelData(IContainerBuilder builder)
        {
            var levelConfig = LevelManager.GetCurrentLevel();
            var waveData = levelConfig.WaveDataConfig;
            var levelGridConfig = levelConfig.LevelGridConfig;
            builder.RegisterInstance(waveData);
            builder.RegisterInstance(levelGridConfig);
            builder.RegisterInstance(levelConfig);
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

        private void ConfigureGameplayManager(IContainerBuilder builder)
        {
            builder.Register<GameplayManager.GameplayManager>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void ConfigureWaves(IContainerBuilder builder)
        {
            builder.RegisterInstance(_waveManager).AsImplementedInterfaces();
            builder.RegisterInstance(_waveSpawner).AsImplementedInterfaces().AsSelf();
        }

        private void ConfigureEnemies(IContainerBuilder builder)
        {
            builder.RegisterInstance(_enemyManager).AsImplementedInterfaces();
        }

        private void ConfigurePlayerBase(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerBasePrefab);
            builder.RegisterInstance(_playerBaseParent);
            builder.Register<PlayerBaseManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerBaseSpawner>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void ConfigureControllers(IContainerBuilder builder)
        {
            builder.Register<VictoryController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DefeatController>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<GameResultController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeStopController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerBaseSpawnController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StartGameController>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void ConfigureUI(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerBaseInfoView);
        }
    }
}