using Game.Gameplay.Storages;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.UI.Storages
{
    public sealed class BulletStorageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _bulletAmount;
        
        private BulletStorage _bulletStorage;
        
        [Inject]
        private void Configure(BulletStorage bulletStorage)
        {
            _bulletStorage = bulletStorage;
            
            _bulletStorage.AmountChanged += BulletStorageOnAmountChanged;
            BulletStorageOnAmountChanged();
        }

        private void BulletStorageOnAmountChanged()
        {
            _bulletAmount.text = $"{_bulletStorage.CurrentAmount} // {_bulletStorage.MaxAmount}";
        }
    }
}