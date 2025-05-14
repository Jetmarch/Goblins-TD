using System;
using Game.GameEngine;
using Game.Gameplay.Controllers;
using VContainer.Unity;

namespace Game.Gameplay.Towers.PlayerBase
{
    internal sealed class PlayerBaseDestroyController : IInitializable, IDisposable
    {
        private readonly IPlayerBaseManager _playerBaseManager;
        private readonly HealthStorage _healthStorage;

        public PlayerBaseDestroyController(IPlayerBaseManager playerBaseManager, HealthStorage healthStorage)
        {
            _playerBaseManager = playerBaseManager;
            _healthStorage = healthStorage;
        }

        public void Initialize()
        {
            _healthStorage.OnDeath += _playerBaseManager.NotifyBaseDestroyed;
        }

        public void Dispose()
        {
            _healthStorage.OnDeath -= _playerBaseManager.NotifyBaseDestroyed;
        }
    }
}