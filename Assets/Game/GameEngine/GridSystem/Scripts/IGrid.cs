
namespace Game.GameEngine.GridSystem
{
    public interface IGrid<TCell> where TCell : ICell
    {
        TCell GetCell(int x, int y);
        int Width { get; }
        int Height { get; }
    }
}