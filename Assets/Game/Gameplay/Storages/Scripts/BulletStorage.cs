using System;
using UnityEngine;

namespace Game.Gameplay.Storages
{
    [Serializable]
    public sealed class BulletStorage
    {
        public event Action AmountChanged;
        public int CurrentAmount => _currentAmount;
        public int MaxAmount => _maxAmount;
        public int MinAmount => _minAmount;
        [SerializeField] private int _currentAmount;
        [SerializeField] private int _maxAmount;
        [SerializeField] private int _minAmount = 0;

        public void Add(int amount)
        {
            _currentAmount += amount;
            _currentAmount = Mathf.Clamp(_currentAmount, _minAmount, _maxAmount);
            AmountChanged?.Invoke();
        }

        public void Decrease(int amount)
        {
            _currentAmount -= amount;
            _currentAmount = Mathf.Clamp(_currentAmount, _minAmount, _maxAmount);
            AmountChanged?.Invoke();
        }
        
        private void SetAmount(int amount)
        {
            _currentAmount = amount;
            _currentAmount = Mathf.Clamp(_currentAmount, _minAmount, _maxAmount);
            AmountChanged?.Invoke();
        }
    }
}