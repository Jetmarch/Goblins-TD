using System;
using Game.GameEngine.Common;
using Game.GameEngine.EntityComponents;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Storages
{
    [Prototype]
    public sealed class AmmoStorage : MonoBehaviour
    {
        public event Action AmountChanged;
        public int CurrentAmount => _currentAmount;
        public int MaxAmount => _maxAmount;
        public int MinAmount => _minAmount;
        [SerializeField] private int _currentAmount;
        [SerializeField] private int _maxAmount;
        [SerializeField] private int _minAmount = 0;

        [SerializeField] private TextMeshPro _ammoAmountText;

        // public AmmoStorage(int maxAmount, int minAmount, int currentAmount = 0)
        // {
        //     _maxAmount = maxAmount;
        //     _minAmount = minAmount;
        //     _currentAmount = currentAmount;
        // }
        
        public void Init(int maxAmount, int minAmount, int currentAmount = 0)
        {
            _maxAmount = maxAmount;
            _minAmount = minAmount;
            SetAmount(currentAmount);
        }

        public bool CanAdd()
        {
            return _currentAmount < _maxAmount;
        }

        public void Add(int amount)
        {
            _currentAmount += amount;
            _currentAmount = Mathf.Clamp(_currentAmount, _minAmount, _maxAmount);
            AmountChanged?.Invoke();

            UpdateAmmoAmountText();
        }

        public void Decrease(int amount)
        {
            _currentAmount -= amount;
            _currentAmount = Mathf.Clamp(_currentAmount, _minAmount, _maxAmount);
            AmountChanged?.Invoke();

            UpdateAmmoAmountText();
        }
        
        private void SetAmount(int amount)
        {
            _currentAmount = amount;
            _currentAmount = Mathf.Clamp(_currentAmount, _minAmount, _maxAmount);
            AmountChanged?.Invoke();

            UpdateAmmoAmountText();
        }
        
        //Temporary
        private void UpdateAmmoAmountText()
        {
            var ammoAmountText = $"{_currentAmount}/{_maxAmount}";
            _ammoAmountText.text = ammoAmountText;
        }
    }
}