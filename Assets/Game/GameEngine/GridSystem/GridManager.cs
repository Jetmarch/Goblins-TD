using System;
using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Transform _gridPosition;   
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;
        [SerializeField] private float _cellSize;
        
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
                
                Debug.Log($"World point: {worldPoint}");
                var cell = _grid.GetCellByPosition(worldPoint.x, worldPoint.y);
                if (cell == null) return;
                
                Debug.Log($"Cell position: {cell.XPos}, {cell.YPos}");
                cell.SetBusy(!cell.IsBusy);
            }
        }
        
        private void OnDrawGizmos()
        {
            if (_grid == null) return;
            Gizmos.color = Color.green;
            
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    var currentCell = _grid.Cells[x, y];
                    var cellSize = currentCell.Size;
                    var halfCellSize = cellSize * 0.5f;
                    var xPos = (_gridPosition.position.x + currentCell.XPos * currentCell.Size) + halfCellSize;
                    var yPos = (_gridPosition.position.y + currentCell.YPos * currentCell.Size) + halfCellSize;
                    
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

        private Cell GetCellByWorldPosition(Vector3 worldPosition)
        {
            var worldX = worldPosition.x;
            var worldY = worldPosition.y;
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    var currentCell = _grid.Cells[x, y];
                    var cellSize = currentCell.Size;
                    var halfCellSize = 0f;// cellSize * 0.5f;
                    var xPos = (_gridPosition.position.x + currentCell.XPos * currentCell.Size) + halfCellSize;
                    var yPos = (_gridPosition.position.y + currentCell.YPos * currentCell.Size) + halfCellSize;
                    var cellPosition = new Vector3(xPos, yPos, 0);


                    if (worldX > xPos && worldX < xPos + cellSize
                                      && worldY > yPos && worldY < yPos + cellSize)
                    {
                        return currentCell;
                    }
                }
            }
            return default;
        }
        

        [ContextMenu("ConstructGrid")]
        public void ConstructGrid()
        {
            var xPos = _gridPosition.position.x;
            var yPos = _gridPosition.position.y;
            _grid = new Grid(xPos, yPos, _gridWidth, _gridHeight, _cellSize);
        }
    }
}