using System;
using UnityEngine;

namespace Game.Gameplay.Towers.PlayerBase
{
    internal sealed class PlayerBaseManager : IPlayerBaseManager
    {
        private readonly IPlayerBaseFactory _playerBaseFactory;
        public event Action BaseDestroyed;
        public event Action BaseCreated;

        private GameObject _playerBase;

        public PlayerBaseManager(IPlayerBaseFactory playerBaseFactory)
        {
            _playerBaseFactory = playerBaseFactory;
        }

        public void NotifyBaseDestroyed()
        {
            BaseDestroyed?.Invoke();
        }

        public void CreatePlayerBase()
        {
            _playerBase = _playerBaseFactory.CreatePlayerBase();
            BaseCreated?.Invoke();
        }

        public GameObject GetPlayerBase()
        {
            if (!_playerBase)
            {
                Debug.LogWarning($"Player base not created yet!");
            }
            
            return _playerBase;
        }
    }
}