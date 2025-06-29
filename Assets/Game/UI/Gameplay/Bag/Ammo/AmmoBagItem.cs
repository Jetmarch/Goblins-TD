using Game.GameEngine.DragAndDrop;
using Game.Gameplay.LevelGrid;
using Game.Gameplay.Storages;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.Bag.Ammo
{
    [RequireComponent(typeof(DragAndDropItem))]
    public sealed class AmmoBagItem : MonoBehaviour
    {
        [SerializeField] private int _amountOfAmmo = 5;
        private DragAndDropItem _dragAndDropItem;
        private LevelGrid _levelGrid;
        private Camera _camera;

        private GameObject _possibleTarget;
        
        private void Start()
        {
            _dragAndDropItem = GetComponent<DragAndDropItem>();
            _dragAndDropItem.Drag += OnDrag;
            _dragAndDropItem.EndDrag += OnEndDrag;
            
            _camera = Camera.main;
            
            var globalGridManager = GameObject.Find("TowerGrid").GetComponent<GridManager>();
            _levelGrid = globalGridManager.Grid;
        }

        private void OnDrag(PointerEventData eventData)
        {
            var worldPoint = _camera.ScreenToWorldPoint(eventData.position);
            
            var currentCell = LevelGridUseCases.GetCellByWorldPositionOrDefault(_levelGrid, worldPoint);
            if (currentCell != default)
            {
                if (currentCell.Occupier)
                {
                    _possibleTarget = currentCell.Occupier;
                    Debug.Log("Possible target: " + _possibleTarget.name);
                }
            }
        }
        
        private void OnEndDrag(PointerEventData eventData, Vector2 startPosition)
        {
            if (!_possibleTarget)
            {
                transform.position = startPosition;
                return;
            }

            var ammoStorage = _possibleTarget.GetComponent<AmmoStorage>();
            if (!ammoStorage)
            {
                transform.position = startPosition;
                return;
            }

            if (!ammoStorage.CanAdd())
            {
                transform.position = startPosition;
                return;
            }
            
            ammoStorage.Add(_amountOfAmmo);
            
            Destroy(gameObject);
        }
    }
}