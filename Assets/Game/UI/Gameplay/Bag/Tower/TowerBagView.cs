using System.Collections.Generic;
using Game.GameEngine.Common;
using UnityEngine;

namespace Game.UI.Bag.Tower
{
    [Prototype]
    public sealed class TowerBagView : BasePanelView
    {
        [SerializeField] private TowerBagItem _dragAndDropItemPrefab;

        [SerializeField] private Transform _bagParent;
        [SerializeField] private List<TowerBagItem> _bagItems; 
        
        public void AddTower(string towerId)
        {
            //TODO: Add tower by id
            //
            
            var newBagItem = Instantiate(_dragAndDropItemPrefab, _bagParent.transform);
            
            _bagItems.Add(newBagItem);
        }
    }
}