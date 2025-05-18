using System;
using Game.GameEngine.GridSystem;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    [Serializable]
    public class LevelGrid : ILevelGrid
    {
        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        public Vector2 CellGap => _cellGap;

        public Vector2 WorldPosition { get; set; }

        public LevelCell[] Cells => _cells;

        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _cellSize;
        [SerializeField] private Vector2 _cellGap;
        [SerializeField] private Vector2 _worldPosition;
        [SerializeField] private LevelCell[] _cells;
        
        public LevelGrid(int width, int height, float cellSize, Vector2 cellGap)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _cellGap = cellGap;
            _cells = new LevelCell[Width * Height];
        }

        public LevelGrid(LevelGrid grid)
        {
            _width = grid.Width;
            _height = grid.Height;
            _cells = new LevelCell[Width * Height];

            CopyCells(grid);
        }
        
        private void CopyCells(LevelGrid grid)
        {
            for(int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _cells[x + (y * Width)] = new LevelCell(grid.GetCell(x, y));
                }
            }
        }
        
        public LevelCell GetCell(int x, int y)
        {
            if (!GridUtilities.CheckGridBounds(x, y, Width, Height))
            {
                Debug.LogWarning($"Cell at {x}, {y} is out of bounds.");
                return default;
            }
            
            var cell = Cells[x + (y * Width)];
            cell.SetWorldPosition(WorldPosition);
            return cell;
        }

        public void SetCell(int x, int y, LevelCell cell)
        {
            if (!GridUtilities.CheckGridBounds(x, y, Width, Height))
            {
                Debug.LogWarning("Cell at " + x + ", " + y + " is out of bounds.");
                return;
            }
            
            Cells[x + (y * Width)] = cell;
        }
    }
}