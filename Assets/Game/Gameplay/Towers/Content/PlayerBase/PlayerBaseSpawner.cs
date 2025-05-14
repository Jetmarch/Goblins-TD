using UnityEngine;
using VContainer.Unity;

namespace Game.Gameplay.Towers.PlayerBase
{
    internal sealed class PlayerBaseSpawner : IStartable
    {
        private readonly GameObject _playerBase;
        private readonly Transform _playerBaseParent;

        public PlayerBaseSpawner(GameObject playerBase, Transform playerBaseParent)
        {
            _playerBase = playerBase;
            _playerBaseParent = playerBaseParent;
        }

        public void Start()
        {
            Object.Instantiate(_playerBase, _playerBaseParent);
        }
    }
}