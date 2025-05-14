using System;
using Game.GameEngine.GameplayManager;
using Game.Gameplay.WaveSystem;
using VContainer.Unity;

namespace Game.Gameplay.Controllers
{
    public sealed class VictoryController  : IInitializable, IDisposable
    {
        private readonly IGameplayManager _gameplayManager;
        private readonly IWaveManager _waveManager;

        public VictoryController(IGameplayManager gameplayManager, IWaveManager waveManager)
        {
            _gameplayManager = gameplayManager;
            _waveManager = waveManager;
        }

        public void Initialize()
        {
            _waveManager.AllWavesComplete += Victory;
        }

        public void Dispose()
        {
            _waveManager.AllWavesComplete -= Victory;
        }

        private void Victory()
        {
            _gameplayManager.EndGameWithResult(GameResult.Victory);
        }
    }
}