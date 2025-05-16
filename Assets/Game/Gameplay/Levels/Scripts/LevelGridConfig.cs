using Game.GameEngine.GridSystem;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "LevelGridConfig", menuName = "Gameplay/Levels/LevelGridConfig")]
    public sealed class LevelGridConfig : ScriptableObject
    {
        public LevelGrid LevelGrid => _levelGrid;
        private LevelGrid _levelGrid;
        
        
        public void SetLevelGrid(LevelGrid levelGrid)
        {
            _levelGrid = levelGrid;
        }
    }
    
    public class LevelCell : ICell
    {
        public LevelCell(int gridX, int gridY, float size, LevelCellType type)
        {
            GridX = gridX;
            GridY = gridY;
            Size = size;
            Type = type;
        }

        public LevelCell(LevelCell cell)
        {
            GridX = cell.GridX;
            GridY = cell.GridY;
            Size = cell.Size;
            Type = cell.Type;
        }

        public int GridX { get; }
        public int GridY { get; }
        public float WorldX { get; }
        public float WorldY { get; }
        public float Size { get; }
        public bool IsWalkable { get; }
        public LevelCellType Type { get; set; }

        public void SetBusy(bool isBusy)
        {
            throw new System.NotImplementedException();
        }
    }

    public enum LevelCellType
    {
        Walkable,
        Buildable
    }

    public class LevelGrid : IGrid<LevelCell>
    {
        public LevelGrid(int width, int height, LevelCell[,] cells)
        {
            Width = width;
            Height = height;
            Cells = cells;
        }

        public LevelGrid(LevelGrid grid)
        {
            Width = grid.Width;
            Height = grid.Height;
            Cells = new LevelCell[Width, Height];

            CopyCells(grid);
        }

        private void CopyCells(LevelGrid grid)
        {
            for(int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[x, y] = new LevelCell(grid.Cells[x, y]);
                }
            }
        }

        public int Width { get; }
        public int Height { get; }
        public Vector2 WorldPosition { get; }
        public LevelCell[,] Cells { get; }
        public LevelCell GetCell(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public void SetPosition(Vector2 worldPosition)
        {
            throw new System.NotImplementedException();
        }
        public LevelCell GetCellByWorldPositionOrDefault(Vector2 worldPosition)
        {
            throw new System.NotImplementedException();
        }
    }
 }