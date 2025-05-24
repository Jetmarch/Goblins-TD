using System;
using UnityEngine;

namespace Game.GameEngine.EntityComponents
{
    [Serializable]
    public class HealthStorage : IComponent
    {
        public event Action HealthChanged;
        public event Action OnDeath;
        
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
        
        [SerializeField] private float _currentHealth;
        
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _minHealth = 0f;

        public void Reset()
        {
            _currentHealth = _maxHealth;
        }

        public void DecreaseHealth(float amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
            HealthChanged?.Invoke();

            if (_currentHealth <= _minHealth)
            {
                OnDeath?.Invoke();
            }
        }

        
    }
}