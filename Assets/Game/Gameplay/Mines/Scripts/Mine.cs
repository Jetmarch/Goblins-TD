using Game.GameEngine.Common;
using Game.Gameplay.Storages;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Game.Gameplay.Mines
{
    [Prototype]
    public sealed class Mine : MonoBehaviour, IPointerDownHandler
    {
        private BulletStorage _bulletStorage;
        [SerializeField] private int _amountOfBulletToAddOnClick = 1;

        [Inject]
        private void Configurate(BulletStorage bulletStorage)
        {
            _bulletStorage = bulletStorage;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _bulletStorage.Add(_amountOfBulletToAddOnClick);
        }
    }
}