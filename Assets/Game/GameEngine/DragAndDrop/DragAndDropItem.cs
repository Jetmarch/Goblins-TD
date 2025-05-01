using System.Collections.Generic;
using System.Linq;
using Game.GameEngine.GridSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Grid = Game.GameEngine.GridSystem.Grid;

namespace Game.GameEngine.DragAndDrop
{
    public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _towerPrefab;
        [SerializeField] private Transform _towerParent;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridManager _globalGridManager;

        [SerializeField] private int _gridWidth = 2;
        [SerializeField] private int _gridHeight = 2;
        [SerializeField] private float _cellSize = 0.5f;
        
        [SerializeField] private float _snapDistance = 0.2f;
        
        private bool _showGrid = false;
        
        private Grid _localGrid;
        
        private Camera _camera;

        private List<Cell> _possibleTargetCells;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _camera = Camera.main;
            _possibleTargetCells = new List<Cell>();
            ConstructGrid();
        }
        
        [ContextMenu("ConstructGrid")]
        public void ConstructGrid()
        {
            var xPos = transform.position.x;
            var yPos = transform.position.y;
            _localGrid = new Grid(transform.position, _gridWidth, _gridHeight, _cellSize);
        }
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _showGrid = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;

            
            var worldPoint = _camera.ScreenToWorldPoint(eventData.position);
            
            
            _localGrid.SetPosition(worldPoint);
            
            var globalGrid = _globalGridManager.Grid;
            _possibleTargetCells.Clear();

            
            foreach (var cell in _localGrid.Cells)
            {
                var cellWorldPositionX = cell.XPos * cell.Size + _localGrid.Position.x ;
                var cellWorldPositionY = cell.YPos * cell.Size + _localGrid.Position.y ;
                var cellWorldPosition = new Vector2(cellWorldPositionX, cellWorldPositionY);
                
                var globalGridCell = globalGrid.GetCellByWorldPosition(cellWorldPosition);
                if (globalGridCell != null && !globalGridCell.IsBusy)
                {
                    _possibleTargetCells.Add(globalGridCell);
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _showGrid = false;

            if (_possibleTargetCells.Count == _localGrid.Cells.Length)
            {

                var towerPosition = GetCenterOfCells(_possibleTargetCells, _globalGridManager.Grid.Position);
                Debug.Log($"towerPosition: {towerPosition}");
                var newTowerOnGrid = Instantiate(_towerPrefab, towerPosition, _towerPrefab.transform.rotation, _towerParent);
                

                foreach (var gridCell in _possibleTargetCells)
                {
                    gridCell.SetBusy(true);
                }

                _possibleTargetCells.Clear();
            }
        }

        private Vector2 GetCenterOfCells(List<Cell> cells, Vector2 gridPosition = default)
        {
            var centerX = cells.Average(cell => cell.XPos * cell.Size + gridPosition.x);
            var centerY = cells.Average(cell => cell.YPos * cell.Size + gridPosition.y);
            
            return new Vector2(centerX, centerY);
        }
        
        private void OnDrawGizmos()
        {
            
            if (!_showGrid) return;
            
            if (_localGrid == null) return;
            Gizmos.color = Color.green;
            
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    var currentCell = _localGrid.Cells[x, y];
                    var cellSize = currentCell.Size;
                    var halfCellSize = cellSize * 0.5f;
                    var xPos = (currentCell.XPos * currentCell.Size) + _localGrid.Position.x + halfCellSize;
                    var yPos = (currentCell.YPos * currentCell.Size) + _localGrid.Position.y + halfCellSize;
                    xPos -= _localGrid.Width * halfCellSize;
                    yPos -= _localGrid.Height * halfCellSize;
                    
                    var position = new Vector3(xPos, yPos, 0);
                    var size = new Vector3(cellSize, cellSize, 0);

                    if (currentCell.IsBusy)
                    {
                        Gizmos.DrawCube(position, size);
                    }
                    else
                    {
                        Gizmos.DrawWireCube(position, size);
                    }
                }
            }
        }
    }
}