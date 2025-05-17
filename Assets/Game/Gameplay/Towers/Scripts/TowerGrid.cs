using Game.GameEngine.Common;
using Game.GameEngine.GridSystem;

namespace Game.Gameplay.Towers
{
    [Prototype]
    public sealed class TowerGrid : IGrid<TowerCell>
    {
        public TowerCell GetCell(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public int Width { get; }
        public int Height { get; }
        public TowerCell[,] Cells { get; }
    }
    [Prototype]
    public sealed class TowerCell : ICell
    {
        public int GridX { get; }
        public int GridY { get; }
        public float Size { get; }
    }
}