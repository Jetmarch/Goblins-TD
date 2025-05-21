using System;
using Game.GameEngine.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Shop
{
    [Prototype]
    public class TowerShopItemView : MonoBehaviour
    {
        public event Action<string> BuyTowerRequest;
        [SerializeField] private Button _buyButton;
        [SerializeField] private string _towerId;

        private void Start()
        {
            _buyButton.onClick.AddListener(BuyTower);
        }

        private void BuyTower()
        {
            BuyTowerRequest?.Invoke(_towerId);
        }
    }
}