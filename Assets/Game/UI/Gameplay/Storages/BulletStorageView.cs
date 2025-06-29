using Game.Gameplay.Storages;
using TMPro;
using UnityEngine;

namespace Game.UI.Storages
{
    //TODO: Use it with towers
    public sealed class BulletStorageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _bulletAmount;
        
        private AmmoStorage _ammoStorage;
        
        private void Configure(AmmoStorage ammoStorage)
        {
            _ammoStorage = ammoStorage;
            
            _ammoStorage.AmountChanged += AmmoStorageOnAmountChanged;
            AmmoStorageOnAmountChanged();
        }

        private void AmmoStorageOnAmountChanged()
        {
            _bulletAmount.text = $"{_ammoStorage.CurrentAmount} // {_ammoStorage.MaxAmount}";
        }
    }
}