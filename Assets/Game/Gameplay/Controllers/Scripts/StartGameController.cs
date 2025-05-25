using Game.GameEngine.GameplayManager;
using VContainer.Unity;

namespace Game.Gameplay.Controllers
{
    public sealed class StartGameController : IStartable
    {
        private readonly IGameplayManager _gameplayManager;

        public StartGameController(IGameplayManager gameplayManager)
        {
            _gameplayManager = gameplayManager;
        }

        public void Start()
        {
            _gameplayManager.StartGame();
        }
    }
}