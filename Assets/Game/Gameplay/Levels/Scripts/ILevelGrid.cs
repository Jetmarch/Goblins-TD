using Game.GameEngine.GridSystem;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    public interface ILevelGrid : IGrid<LevelCell>
    {
        float CellSize { get; }
        Vector2 CellGap { get; } 
        Vector2 WorldPosition { get; set; }
    }
}