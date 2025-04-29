using System;
using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public class GridManager : MonoBehaviour
    {
        public Grid Grid => _grid;
        
        [SerializeField] private Transform _gridPosition;   
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;
        [SerializeField] private float _cellSize;
        
        [SerializeField] private bool _showGrid;
        
        private Grid _grid;
        private Camera _camera;
        
        #region Unity Callbacks
        private void Start()
        {
            ConstructGrid();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

                var cell = _grid.GetCellByWorldPosition(worldPoint);
                cell?.SetBusy(!cell.IsBusy);
            }
            
            _grid.SetPosition(_gridPosition.position);
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
                    // xPos -= _grid.Width * halfCellSize;
                    // yPos -= _grid.Height * halfCellSize;
                    
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
        #endregion

        [ContextMenu("ConstructGrid")]
        public void ConstructGrid()
        {
            var xPos = _gridPosition.position.x;
            var yPos = _gridPosition.position.y;
            _grid = new Grid(_gridPosition.position, _gridWidth, _gridHeight, _cellSize);
        }
    }
}