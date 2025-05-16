using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public interface IGrid<T> where T : ICell
    {
        T GetCell(int x, int y);
        Vector2 WorldPosition { get; }
        void SetPosition(Vector2 worldPosition);
        int Width { get; }
        int Height { get; }
        T GetCellByWorldPositionOrDefault(Vector2 worldPosition);
        T[,] Cells { get; }
    }
}