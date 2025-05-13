using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public interface IGrid
    {
        ICell GetCell(int x, int y);
        Vector2 WorldPosition { get; }
        
        int Width { get; }
        int Height { get; }
        ICell GetCellByWorldPositionOrDefault(Vector2 worldPosition);
        ICell[,] Cells { get; }
    }
}