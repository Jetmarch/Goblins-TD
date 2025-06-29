using System;
using Game.GameEngine.DragAndDrop;
using Game.Gameplay.LevelGrid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.Bag.Tower
{
    [RequireComponent(typeof(DragAndDropItem))]
    public sealed class TowerBagItem : MonoBehaviour
    {
        [SerializeField] private GameObject _towerPrefab;
        
        private Camera _camera;
        private DragAndDropItem _dragAndDropItem;
        private Transform _towerParent;
        private GridView _gridView;
        private LevelGrid _levelGrid;
        private ILevelCell _possibleCellForBuilding;
        
        private void Start()
        {
            _dragAndDropItem = GetComponent<DragAndDropItem>();
            _dragAndDropItem.Drag += OnDrag;
            _dragAndDropItem.EndDrag += OnEndDrag;
            _camera = Camera.main;
            
            _gridView = GameObject.Find("GridView").GetComponent<GridView>();
            var globalGridManager = GameObject.Find("TowerGrid").GetComponent<GridManager>();
            _levelGrid = globalGridManager.Grid;
            _towerParent = GameObject.Find("Towers").transform;
        }

        private void OnDrag(PointerEventData eventData)
        {
            var worldPoint = _camera.ScreenToWorldPoint(eventData.position);
            
            _gridView.UnhighlightAllCells();
            
            _possibleCellForBuilding = LevelGridUseCases.GetCellByWorldPositionOrDefault(_levelGrid, worldPoint);

            if (_possibleCellForBuilding != default)
            {
                _gridView.HighlightCell(_possibleCellForBuilding.GridX, _possibleCellForBuilding.GridY);
            }
        }
        
        private void OnEndDrag(PointerEventData eventData, Vector2 startPosition)
        {
            if (_possibleCellForBuilding == default)
            {
                transform.position = startPosition;
                return;
            }

            if (!LevelGridUseCases.CanBuild(_possibleCellForBuilding))
            {
                transform.position = startPosition;
                return;
            }
            
            var towerPosition = LevelGridUseCases.GetCenterOfCell(_possibleCellForBuilding);
            var newTowerOnGrid = Instantiate(_towerPrefab, towerPosition, _towerPrefab.transform.rotation, _towerParent);
            
            _gridView.SetCellOccupier(_possibleCellForBuilding.GridX, _possibleCellForBuilding.GridY, newTowerOnGrid);
            
            Destroy(gameObject);
        }
    }
}