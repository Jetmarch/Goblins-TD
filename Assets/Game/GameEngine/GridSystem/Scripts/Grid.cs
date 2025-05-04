using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public class Grid : IGrid
    {
        public ICell[,] Cells => _cells;
        public Vector2 Position => _position;
        public Vector2 CellGap => _cellGap;
        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        
        private Vector2 _position;
        
        private readonly ICell[,] _cells;
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        private readonly Vector2 _cellGap;
        
        public Grid(Vector2 position, int width, int height, float cellSize, Vector2 cellGap)
        {
            _position = position;
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _cells = new ICell[width, height];
            _cellGap = cellGap;

            ConstructGrid();
        }

        private void ConstructGrid()
        {
            var currentXGap = 0f;
            var currentYGap = 0f;
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var cell = new Cell(x, y, x + currentXGap, y + currentYGap, _cellSize);
                    _cells[x, y] = cell;
                    currentYGap += _cellGap.y;
                }

                currentYGap = 0f;
                currentXGap += _cellGap.x;
            }
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public ICell GetCellByWorldPositionOrDefault(Vector2 worldPosition)
        {
            GetGridPosition(worldPosition, out var gridX, out var gridY);
            
            return GetCell(gridX, gridY);
        }

        public ICell GetCell(int x, int y)
        {
            return !CheckBounds(x, y, _width, _height) ? default : _cells[x, y];
        }

        private void GetGridPosition(Vector2 worldPosition, out int gridX, out int gridY)
        {
            gridX = Mathf.FloorToInt((worldPosition.x - _position.x - _cellGap.x) / _cellSize);
            gridY = Mathf.FloorToInt((worldPosition.y - _position.y - _cellGap.y) / _cellSize);
        }
        
        //TODO: Move to extensions or utils
        private static bool CheckBounds(int gridX, int gridY, int width, int height)
        {
            if (gridX < 0 || gridX >= width || gridY < 0 || gridY >= height) return false;
            return true;
        }
    }
}