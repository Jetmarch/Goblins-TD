using System;
using Game.GameEngine.GameplayManager;
using Game.Gameplay.Conditions;
using Game.Gameplay.Enemies;
using Game.Gameplay.WaveSystem;
using UnityEngine;
using VContainer.Unity;

namespace Game.Gameplay.Controllers
{
    public sealed class VictoryController  : IInitializable, IDisposable
    {
        private readonly IGameplayManager _gameplayManager;
        private readonly IEnemyManager _enemyManager;

        public VictoryController(IGameplayManager gameplayManager, IEnemyManager enemyManager)
        {
            _gameplayManager = gameplayManager;
            _enemyManager = enemyManager;
        }

        public void Initialize()
        {
            _enemyManager.AllEnemiesKilled += AllEnemiesKilled;
        }

        public void Dispose()
        {
            _enemyManager.AllEnemiesKilled -= AllEnemiesKilled;
        }


        private void AllEnemiesKilled()
        {
            Debug.Log("AllEnemiesKilled");
            CheckVictoryConditions();
        }
        
        private void CheckVictoryConditions()
        {
            if (_enemyManager.IsAllEnemiesKilled)
            {
                Victory();
            }
        }
        
        private void Victory()
        {
            _gameplayManager.EndGameWithResult(GameResult.Victory);
        }
    }
}