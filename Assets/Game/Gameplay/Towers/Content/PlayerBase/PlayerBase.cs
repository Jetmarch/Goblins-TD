using System;
using Game.GameEngine;
using Game.GameEngine.Common;
using Game.GameEngine.EntityComponents;
using UnityEngine;

namespace Game.Gameplay.Towers.PlayerBase
{
    [Prototype, Obsolete]
    public sealed class PlayerBase : MonoBehaviour
    {
        [SerializeField] private HealthStorage _healthStorage;

        private void Start()
        {
            _healthStorage.Reset();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<BasicEnemy>(out var enemy))
            {
                Debug.Log("Player base damaged!");
                _healthStorage.DecreaseHealth(1f);
            }
        }
    }
}