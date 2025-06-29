using System.Collections.Generic;
using Game.GameEngine.Common;
using UnityEngine;

namespace Game.UI.Bag.Ammo
{
    [Prototype]
    public sealed class AmmoBagView : BasePanelView
    {
        [SerializeField] private AmmoBagItem _dragAndDropItemPrefab;

        [SerializeField] private Transform _bagParent;
        [SerializeField] private List<AmmoBagItem> _bagItems; 
        
        public void AddAmmo(string ammoId)
        {
            //TODO: Add tower by id
            //
            
            var newBagItem = Instantiate(_dragAndDropItemPrefab, _bagParent.transform);
            
            _bagItems.Add(newBagItem);
        }
    }
}