using UnityEngine;

namespace Game.Gameplay.Towers.PlayerBase
{
    internal sealed class PlayerBaseSpawner : IPlayerBaseFactory
    {
        private readonly GameObject _playerBasePrefab;
        private readonly Transform _playerBaseParent;

        public PlayerBaseSpawner(GameObject playerBasePrefab, Transform playerBaseParent)
        {
            _playerBasePrefab = playerBasePrefab;
            _playerBaseParent = playerBaseParent;
        }

        public GameObject CreatePlayerBase()
        {
            return Object.Instantiate(_playerBasePrefab, _playerBaseParent);
        }
    }
}