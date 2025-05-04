namespace Game.GameEngine.GridSystem
{
    public interface IGrid
    {
        ICell GetCell(int x, int y);
    }
}