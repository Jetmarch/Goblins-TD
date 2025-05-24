using Game.Gameplay.Towers.PlayerBase;
using VContainer.Unity;

namespace Game.Gameplay.Controllers
{
    public sealed class PlayerBaseSpawnController : IStartable
    {
        private readonly IPlayerBaseManager _playerBaseManager;

        public PlayerBaseSpawnController(IPlayerBaseManager playerBaseManager)
        {
            _playerBaseManager = playerBaseManager;
        }

        public void Start()
        {
            _playerBaseManager.CreatePlayerBase();
        }
    }
}