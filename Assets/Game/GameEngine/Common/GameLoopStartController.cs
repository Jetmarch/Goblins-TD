using System;
using Game.GameEngine.GameplayManager;
using Modules.Core.GameLoop;
using VContainer.Unity;

namespace Game.GameEngine.Common
{
    public sealed class GameLoopStartController : IInitializable, IDisposable
    {
        private readonly IGameLoopManager _gameLoopManager;
        private readonly IGameplayManager _gameplayManager;

        public GameLoopStartController(IGameLoopManager gameLoopManager, IGameplayManager gameplayManager)
        {
            _gameLoopManager = gameLoopManager;
            _gameplayManager = gameplayManager;
        }

        public void Start()
        {
            _gameLoopManager.StartGame();
        }

        public void Initialize()
        {
            _gameplayManager.GameStart += _gameLoopManager.StartGame;
        }

        public void Dispose()
        {
            _gameplayManager.GameStart -= _gameLoopManager.StartGame;
        }
    }
}