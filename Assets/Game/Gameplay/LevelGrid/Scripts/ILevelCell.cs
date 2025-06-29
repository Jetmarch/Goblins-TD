using Game.GameEngine.GridSystem;
using UnityEngine;

namespace Game.Gameplay.LevelGrid
{
    public interface ILevelCell : ICell
    {
        float WorldX { get; }
        float WorldY { get; }
        bool IsBusy { get; }
        LevelCellType Type { get; }
        bool IsWalkable { get; }
        void SetType(LevelCellType type);
        void SetOccupier(GameObject occupier);
        GameObject Occupier { get; }
    }
}