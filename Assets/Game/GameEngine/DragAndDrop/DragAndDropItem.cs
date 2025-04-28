using Game.GameEngine.GridSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Grid = Game.GameEngine.GridSystem.Grid;

namespace Game.GameEngine.DragAndDrop
{
    public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridManager _gridManager;

        [SerializeField] private int _gridWidth = 2;
        [SerializeField] private int _gridHeight = 2;
        [SerializeField] private float _cellSize = 0.5f;
        
        [SerializeField] private float _snapDistance = 0.2f;
        
        private bool _showGrid = false;
        
        private Grid _grid;
        
        private Camera _camera;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            _camera = Camera.main;

            ConstructGrid();
        }
        
        [ContextMenu("ConstructGrid")]
        public void ConstructGrid()
        {
            var xPos = transform.position.x;
            var yPos = transform.position.y;
            _grid = new Grid(transform.position, _gridWidth, _gridHeight, _cellSize);
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
            Debug.Log(worldPoint);
            if (_gridManager.IsGridInBounds(_grid))
            {
                foreach (var cell in _grid.Cells)
                {
                    cell.SetBusy(true);
                }
            }
            else
            {
                foreach (var cell in _grid.Cells)
                {
                    cell.SetBusy(false);
                }
            }
            
            
            _grid.SetPosition(worldPoint);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _showGrid = false;
        }
        
        private void OnDrawGizmos()
        {
            if (!_showGrid) return;
            
            if (_grid == null) return;
            Gizmos.color = Color.green;
            
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    var currentCell = _grid.Cells[x, y];
                    var cellSize = currentCell.Size;
                    var halfCellSize = cellSize * 0.5f;
                    var xPos = (currentCell.XPos * currentCell.Size) + _grid.Position.x + halfCellSize;
                    var yPos = (currentCell.YPos * currentCell.Size) + _grid.Position.y + halfCellSize;
                    xPos -= _grid.Width * halfCellSize;
                    yPos -= _grid.Height * halfCellSize;
                    
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