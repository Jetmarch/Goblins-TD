using Game.GameEngine.GridSystem;

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
        void SetBusy(bool isBusy);
    }
}