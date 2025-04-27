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

        public Cell GetCellByPosition(float worldX, float worldY)
        {
            var halfCellSize = _cellSize * 0.5f;
            var widthOffset = _width * halfCellSize;
            var heightOffset = _height * halfCellSize;

            var centeredXPosition = _position.x - widthOffset;
            var centeredYPosition = _position.y - heightOffset;
            
            if (!CheckBounds(centeredXPosition, centeredYPosition,
                    _width * _cellSize,
                    _height * _cellSize,
                    worldX, worldY))
            {
                Debug.LogWarning("Out of bounds");
                return default;
            }
            
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var currentCell = _cells[x, y];
                    var cellXPos = (centeredXPosition + currentCell.XPos * currentCell.Size);
                    var cellYPos = (centeredYPosition + currentCell.YPos * currentCell.Size);

                    if (CheckBounds(cellXPos, cellYPos, _cellSize, _cellSize, worldX, worldY))
                    {
                        return currentCell;
                    }
                }
            }
            return default;
        }
        
        //TODO: Move to extensions or utils
        private static bool CheckBounds(float aX, float aY, float aWidth, float aHeight, float bX, float bY)
        {
            return (bX > aX && bX < aX + aWidth)
                           && (bY > aY && bY < aY + aHeight);
        }
    }
}