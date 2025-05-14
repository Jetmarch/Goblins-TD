using System;
using Game.GameEngine.GameplayManager;
using VContainer.Unity;

namespace Game.Gameplay.Controllers
{
    public sealed class DefeatController : IInitializable, IDisposable
    {
        private readonly IGameplayManager _gameplayManager;
        private readonly IPlayerBaseManager _playerBaseManager;

        public DefeatController(IPlayerBaseManager playerBaseManager, IGameplayManager gameplayManager)
        {
            _playerBaseManager = playerBaseManager;
            _gameplayManager = gameplayManager;
        }

        public void Initialize()
        {
            _playerBaseManager.BaseDestroyed += Defeat;
        }

        public void Dispose()
        {
            _playerBaseManager.BaseDestroyed -= Defeat;
        }
        
        private void Defeat()
        {
            _gameplayManager.EndGameWithResult(GameResult.Defeat);
        }
    }

    public interface IPlayerBaseManager
    {
        event Action BaseDestroyed;
        void NotifyBaseDestroyed();
    }
}