using System;
using UnityEngine;

namespace Game.Gameplay.Coins
{
    [Serializable]
    internal sealed class CoinsStorage
    {
        public event Action CoinsChanged;
        
        public int Coins => _coins;
        [SerializeField] private int _coins = 0;

        public void DecreaseCoins(int amount)
        {
            if (!HasEnoughCoins(amount))
            {
                return;
            }
            _coins -= amount;
            CoinsChanged?.Invoke();
        }

        public void AddCoins(int amount)
        {
            _coins += amount;
            CoinsChanged?.Invoke();
        }
        
        public bool HasEnoughCoins(int amount) => _coins >= amount;
    }
}