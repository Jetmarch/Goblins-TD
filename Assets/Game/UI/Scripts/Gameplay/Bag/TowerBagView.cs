using System.Collections.Generic;
using Game.GameEngine.Common;
using Game.GameEngine.DragAndDrop;
using UnityEngine;

namespace Game.UI.Bag
{
    [Prototype]
    public sealed class TowerBagView : BasePanelView
    {
        [SerializeField] private DragAndDropItem _dragAndDropItemPrefab;

        [SerializeField] private Transform _bagParent;
        [SerializeField] private List<DragAndDropItem> _bagItems; 
        
        public void AddTower(string towerId)
        {
            //TODO: Add tower by id
            //
            
            var newBagItem = Instantiate(_dragAndDropItemPrefab, _bagParent.transform);
            
            _bagItems.Add(newBagItem);
        }
    }
}