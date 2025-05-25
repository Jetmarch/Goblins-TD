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
            _gameplayManager.GameStart += ResumeTime;
        }

        public void Dispose()
        {
            _gameplayManager.GameEnd -= StopTime;
            _gameplayManager.GameStart -= ResumeTime;
        }

        private void StopTime(GameResult _)
        {
            Time.timeScale = 0;
        }
        
        private void ResumeTime()
        {
            Time.timeScale = 1;
        }
    }
}