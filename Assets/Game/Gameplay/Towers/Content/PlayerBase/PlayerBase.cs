using System;
using Game.GameEngine;
using UnityEngine;

namespace Game.Gameplay.Towers.PlayerBase
{
    public class PlayerBase : MonoBehaviour
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