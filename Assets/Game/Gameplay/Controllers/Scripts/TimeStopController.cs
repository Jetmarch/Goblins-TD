using System;
using Game.GameEngine.GameplayManager;
using UnityEngine;
using VContainer.Unity;

namespace Game.Gameplay.Controllers
{
    public sealed class TimeStopController : IInitializable, IDisposable
    {
        private readonly IGameplayManager _gameplayManager;

        public TimeStopController(IGameplayManager gameplayManager)
        {
            _gameplayManager = gameplayManager;
        }

        public void Initialize()
        {
            _gameplayManager.GameEnd += StopTime;
        }

        public void Dispose()
        {
            _gameplayManager.GameEnd -= StopTime;
        }

        private void StopTime(GameResult _)
        {
            Time.timeScale = 0;
        }
    }
}