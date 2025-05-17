using System.Collections.Generic;
using Game.GameEngine.Common;
using Game.Gameplay.Levels;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.GameEngine.DragAndDrop
{
    [RequireComponent(typeof(GridManager)), RequireComponent(typeof(CanvasGroup))]
    [Prototype]
    public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _towerPrefab;
        [SerializeField] private Transform _towerParent;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridManager _globalGridManager;
        [SerializeField] private GridManager _towerGridManager;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _onDragAlpha = 0.35f;
        
        [SerializeField] private GridView _gridView;
        
        private Camera _camera;

        private List<ILevelCell> _possibleTargetCells;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _towerGridManager = GetComponent<GridManager>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _camera = Camera.main;
            _possibleTargetCells = new List<ILevelCell>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _towerGridManager.ShowGrid();
            _canvasGroup.alpha = _onDragAlpha;
            _gridView.Show();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;

            var worldPoint = _camera.ScreenToWorldPoint(eventData.position);
            
            _towerGridManager.UpdateGridWorldPosition(worldPoint);
            
            var globalGrid = _globalGridManager.Grid;
            
            _gridView.UnhighlightAllCells();
            
            LevelGridUseCases.GetPossibleTargetCellsForBuilding(_towerGridManager.Grid, globalGrid, _possibleTargetCells);
            foreach (var cell in _possibleTargetCells)
            {
                _gridView.HighlightCell(cell.GridX, cell.GridY);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _towerGridManager.HideGrid();
            _canvasGroup.alpha = 1f;
            _gridView.Hide();

            if (!LevelGridUseCases.CanBuild(_possibleTargetCells, _towerGridManager.Grid)) return;
            
            var towerPosition = LevelGridUseCases.GetCenterOfCells(_possibleTargetCells);
            var newTowerOnGrid = Instantiate(_towerPrefab, towerPosition, _towerPrefab.transform.rotation, _towerParent);

            foreach (var gridCell in _possibleTargetCells)
            {
                _gridView.SetCellBusy(gridCell.GridX, gridCell.GridY, true);
            }
            _possibleTargetCells.Clear();
            
            Destroy(gameObject);
        }
    }
}