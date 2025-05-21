using System;
using System.Collections.Generic;
using Game.GameEngine.Common;
using Game.Gameplay.Coins;
using Game.UI.Bag;
using UnityEngine;

namespace Game.UI.Shop
{
    [Prototype]
    public sealed class TowerShopView : BasePanelView
    {
        public event Action NotEnoughCoins;
        [SerializeField] private List<TowerShopItemView> _towerShopItems;

        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private TowerBagView _towerBagView;

        private void Start()
        {
            if (_towerShopItems.Count <= 0)
            {
                Debug.LogWarning("TowerShopItems is empty");
                return;
            }

            foreach (var towerShopItem in _towerShopItems)
            {
                towerShopItem.BuyTowerRequest += OnBuyTowerRequest;
            }
        }

        private void OnBuyTowerRequest(string towerId)
        {
            if (!_coinsView.CoinsStorage.HasEnoughCoins(10))
            {
                Debug.Log("Not enough coins");
                NotEnoughCoins?.Invoke();
                return;
            }
            
            _coinsView.CoinsStorage.RemoveCoins(10);
            
            _towerBagView.AddTower(towerId);
        }
    }
}