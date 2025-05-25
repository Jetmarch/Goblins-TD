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
        private readonly IWaveManager _waveManager;
        private readonly IEnemyManager _enemyManager;

        public VictoryController(IGameplayManager gameplayManager, IWaveManager waveManager, IEnemyManager enemyManager)
        {
            _gameplayManager = gameplayManager;
            _waveManager = waveManager;
            _enemyManager = enemyManager;
        }

        public void Initialize()
        {
            _waveManager.AllWavesComplete += AllWavesComplete;
            _enemyManager.AllEnemiesKilled += AllEnemiesKilled;
        }

        public void Dispose()
        {
            _waveManager.AllWavesComplete -= AllWavesComplete;
            _enemyManager.AllEnemiesKilled -= AllEnemiesKilled;
        }


        private void AllEnemiesKilled()
        {
            Debug.Log("AllEnemiesKilled");
            CheckVictoryConditions();
        }
        
        private void AllWavesComplete()
        {
            Debug.Log("AllWavesComplete");
            CheckVictoryConditions();
        }
        
        private void CheckVictoryConditions()
        {
            if (VictoryConditionsUseCases.IsVictory(_waveManager, _enemyManager))
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