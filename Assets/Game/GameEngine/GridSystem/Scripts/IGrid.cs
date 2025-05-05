using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public interface IGrid
    {
        ICell GetCell(int x, int y);
        Vector2 WorldPosition { get; } 
    }
}