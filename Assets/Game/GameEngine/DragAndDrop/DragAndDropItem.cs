using System.Collections.Generic;
using Game.GameEngine.GridSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.GameEngine.DragAndDrop
{
    [RequireComponent(typeof(GridManager)), RequireComponent(typeof(CanvasGroup))]
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

        private List<Cell> _possibleTargetCells;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _towerGridManager = GetComponent<GridManager>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _camera = Camera.main;
            _possibleTargetCells = new List<Cell>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
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
            
            GridUseCases.GetPossibleTargetCellsForBuilding(_towerGridManager.Grid, globalGrid, _possibleTargetCells);
            foreach (var cell in _possibleTargetCells)
            {
                _gridView.HighlightCell(cell.GridPosX, cell.GridPosY);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _towerGridManager.HideGrid();
            _canvasGroup.alpha = 1f;
            _gridView.Hide();

            if (!GridUseCases.CanBuild(_possibleTargetCells, _towerGridManager.Grid)) return;
            
            var towerPosition = GridUseCases.GetCenterOfCells(_possibleTargetCells, _globalGridManager.Grid.Position);
            var newTowerOnGrid = Instantiate(_towerPrefab, towerPosition, _towerPrefab.transform.rotation, _towerParent);

            foreach (var gridCell in _possibleTargetCells)
            {
                _gridView.SetCellBusy(gridCell.GridPosX, gridCell.GridPosY, true);
            }
            _possibleTargetCells.Clear();
            
            Destroy(gameObject);
        }
    }
}