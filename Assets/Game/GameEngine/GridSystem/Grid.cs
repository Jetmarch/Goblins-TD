using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public class Grid
    {
        public Cell[,] Cells => _cells;
        public Vector2 Position => _position;
        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        
        private Vector2 _position;
        
        private readonly Cell[,] _cells;
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        
        public Grid(Vector2 position, int width, int height, float cellSize)
        {
            _position = position;
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _cells = new Cell[width, height];
            
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var cell = new Cell(x, y, _cellSize);
                    _cells[x, y] = cell;
                }
            }
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public Cell GetCellByWorldPosition(Vector2 worldPosition)
        {
            GetGridPosition(worldPosition, out var gridX, out var gridY);
            
            return GetCell(gridX, gridY);
        }

        private Cell GetCell(int x, int y)
        {
            return !CheckBounds(x, y, _width, _height) ? default : _cells[x, y];
        }

        private void GetGridPosition(Vector2 worldPosition, out int gridX, out int gridY)
        {
            gridX = Mathf.FloorToInt((worldPosition.x - _position.x) / _cellSize);
            gridY = Mathf.FloorToInt((worldPosition.y - _position.y) / _cellSize);
        }
        
        //TODO: Move to extensions or utils
        private static bool CheckBounds(int gridX, int gridY, int width, int height)
        {
            if (gridX < 0 || gridX >= width || gridY < 0 || gridY >= height) return false;
            return true;
        }
    }
}