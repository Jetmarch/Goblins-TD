using System.Collections.Generic;
using Game.GameEngine.Common;
using Game.Gameplay.LevelGrid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.GameEngine.DragAndDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    [Prototype]
    public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _towerPrefab;
        [SerializeField] private Transform _towerParent;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridManager _globalGridManager;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _onDragAlpha = 0.35f;
        
        [SerializeField] private GridView _gridView;
        
        private Camera _camera;

        private ILevelCell _possibleCellForBuilding;

        private Vector2 _startPosition;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _camera = Camera.main;
            
            _gridView = GameObject.Find("GridView").GetComponent<GridView>();
            _globalGridManager = GameObject.Find("TowerGrid").GetComponent<GridManager>();
            _towerParent = GameObject.Find("Towers").transform;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.position;
            _canvasGroup.alpha = _onDragAlpha;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;

            var worldPoint = _camera.ScreenToWorldPoint(eventData.position);
            
            var globalGrid = _globalGridManager.Grid;
            
             _gridView.UnhighlightAllCells();
            
            _possibleCellForBuilding = LevelGridUseCases.GetCellByWorldPositionOrDefault(globalGrid, worldPoint);

            if (_possibleCellForBuilding != default)
            {
                _gridView.HighlightCell(_possibleCellForBuilding.GridX, _possibleCellForBuilding.GridY);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            if (_possibleCellForBuilding == default)
            {
                transform.position = _startPosition;
                return;
            }

            if (!LevelGridUseCases.CanBuild(_possibleCellForBuilding))
            {
                transform.position = _startPosition;
                return;
            }
            
            var towerPosition = LevelGridUseCases.GetCenterOfCell(_possibleCellForBuilding);
            var newTowerOnGrid = Instantiate(_towerPrefab, towerPosition, _towerPrefab.transform.rotation, _towerParent);
            
            _gridView.SetCellBusy(_possibleCellForBuilding.GridX, _possibleCellForBuilding.GridY, true);
            
            Destroy(gameObject);
        }
    }
}